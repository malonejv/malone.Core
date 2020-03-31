using AutoMapper;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler.Implementations;
using malone.Core.CL.Exceptions.Manager.Implementations;
using malone.Core.DAL.AdoNet.Context;
using malone.Core.DAL.Base.Repositories;
using malone.Core.DAL.Base.UnitOfWork;
using malone.Core.EL;
using malone.Core.EL.Filters;
using malone.Core.EL.Filters.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace malone.Core.DAL.AdoNet.Repositories.Implementations
{
    public abstract class AdoNetRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        private AdoNetContext _context;

        protected AdoNetContext Context => _context;

        //protected IDbCommand _command;

        protected IUnitOfWork UnitOfWork { get; private set; }
        protected Mapper Mapper { get; private set; }


        public AdoNetRepository(IUnitOfWork unitOfWork, Mapper mapper)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            UnitOfWork = unitOfWork;
            _context = (AdoNetContext)UnitOfWork.Context;

            Mapper = mapper;
        }

        protected IQueryable<TEntity> GetQueryable(
           IDbCommand command,
           bool includeDeleted = false,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            try
            {

                if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
                {
                    Context.Db.AddParameterIsDeleted(command, includeDeleted);
                }

                IDataAdapter adapter = Context.Db.CreateAdapter(command);
                DataSet ds = new DataSet();

                adapter.Fill(ds);

                command.Dispose();
                //TODO: DISPOSE COMMAND

                List<TEntity> result = new List<TEntity>();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    TEntity entityMapped;
                    foreach (var dr in ds.Tables[0].Rows)
                    {
                        entityMapped = Mapper.Map<TEntity>(dr);
                        result.Add(entityMapped);
                    }
                }

                var query = result.AsQueryable<TEntity>();
                if (orderBy != null && result.Count > 0)
                {
                    return orderBy(query);
                }
                else
                {
                    return query;
                }

            }
            catch (Exception ex)
            {
                //TODO: Mejorar manejo de excepciones

                var messageManager = new ExceptionMessageManager();
                var message = string.Format(messageManager.GetDescription((int)CoreErrors.E400), typeof(TEntity));
                var dex = new DataAccessException((int)CoreErrors.E400, message, ex);

                var exceptionHandler = new ExceptionHandler(null);
                exceptionHandler.HandleException(dex);
            }
            return null;
        }

        protected KeyValuePair<CommandType, string> validateCommandText(KeyValuePair<CommandType, string> commandTextConfig)
        {
            if (commandTextConfig.Value == null) throw new ArgumentNullException(nameof(commandTextConfig.Value));
            if (commandTextConfig.Value == string.Empty) throw new ArgumentException(nameof(commandTextConfig.Value));

            string commandText = "";
            bool hasSemicolon = commandTextConfig.Value.Last() == ';';
            if (commandTextConfig.Key == CommandType.Text)
            {
                if (!hasSemicolon)
                {
                    commandText = commandTextConfig.Value + ';';
                }
            }
            else
            {
                if (hasSemicolon)
                {
                    var indSemicolon = commandTextConfig.Value.LastIndexOf(';');
                    commandText = commandTextConfig.Value.Substring(0, indSemicolon);
                }
            }
            if (!string.IsNullOrEmpty(commandText))
                return new KeyValuePair<CommandType, string>(commandTextConfig.Key, commandText);
            else
                return commandTextConfig;
        }

        protected abstract KeyValuePair<CommandType, string> ConfigureGetCommandText(bool includeDeleted, string includeProperties);

        public virtual IEnumerable<TEntity> Get<TFilter>(
           TFilter filter = default(TFilter),
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false,
           string includeProperties = "")
            where TFilter : class, IFilterExpression
        {
            try
            {
                IQueryable<TEntity> query;

                using (var connection = Context.Db.CreateConnection())
                {
                    connection.Open();

                    var commandTextConfig = ConfigureGetCommandText(includeDeleted, includeProperties);
                    commandTextConfig = validateCommandText(commandTextConfig);
                    CommandType type = commandTextConfig.Key;
                    string commandText = commandTextConfig.Value;

                    var command = Context.Db.CreateCommand(connection, type, commandText);

                    if (filter != default(TFilter))
                        (filter as IFilterExpressionAdoNet).SetConfiguredParameters(command, Context.Db);

                    query = GetQueryable(command, includeDeleted, orderBy);
                }

                return query.ToList<TEntity>();
            }
            catch (Exception ex)
            {
                //TODO: Mejorar manejo de excepciones

                var messageManager = new ExceptionMessageManager();
                var message = string.Format(messageManager.GetDescription((int)CoreErrors.E400), typeof(TEntity));
                var dex = new DataAccessException((int)CoreErrors.E400, message, ex);

                var exceptionHandler = new ExceptionHandler(null);
                exceptionHandler.HandleException(dex);
            }
            return null;
        }

        protected abstract KeyValuePair<CommandType, string> ConfigureGetAllCommandText(bool includeDeleted, string includeProperties);

        public virtual IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = ""
           )
        {
            try
            {
                IQueryable<TEntity> query;

                using (var connection = Context.Db.CreateConnection())
                {
                    connection.Open();

                    var commandTextConfig = ConfigureGetAllCommandText(includeDeleted, includeProperties);
                    commandTextConfig = validateCommandText(commandTextConfig);
                    CommandType type = commandTextConfig.Key;
                    string commandText = commandTextConfig.Value;

                    var command = Context.Db.CreateCommand(connection, type, commandText);

                    query = GetQueryable(command, includeDeleted, orderBy);
                }

                return query.ToList<TEntity>();
            }
            catch (Exception ex)
            {
                //TODO: Mejorar manejo de excepciones

                var messageManager = new ExceptionMessageManager();
                var message = string.Format(messageManager.GetDescription((int)CoreErrors.E400), typeof(TEntity));
                var dex = new DataAccessException((int)CoreErrors.E400, message, ex);

                var exceptionHandler = new ExceptionHandler(null);
                exceptionHandler.HandleException(dex);
            }
            return null;
        }

        protected abstract KeyValuePair<CommandType, string> ConfigureGetByIdCommandText(bool includeDeleted, string includeProperties);

        public virtual TEntity GetById(
            object id,
            bool includeDeleted = false,
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query;

                using (var connection = Context.Db.CreateConnection())
                {
                    connection.Open();

                    var commandTextConfig = ConfigureGetByIdCommandText(includeDeleted, includeProperties);
                    commandTextConfig = validateCommandText(commandTextConfig);
                    CommandType type = commandTextConfig.Key;
                    string commandText = commandTextConfig.Value;

                    var command = Context.Db.CreateCommand(connection, type, commandText);

                    Context.Db.AddParameterId(command, id);

                    query = GetQueryable(command, includeDeleted);
                }

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                //TODO: Mejorar manejo de excepciones

                var messageManager = new ExceptionMessageManager();
                var message = string.Format(messageManager.GetDescription((int)CoreErrors.E400), typeof(TEntity));
                var dex = new DataAccessException((int)CoreErrors.E400, message, ex);

                var exceptionHandler = new ExceptionHandler(null);
                exceptionHandler.HandleException(dex);
            }
            return null;
        }

        protected abstract KeyValuePair<CommandType, string> ConfigureGetEntityCommandText(bool includeDeleted, string includeProperties);

        public virtual TEntity GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = "")
            where TFilter : class, IFilterExpression
        {
            throw new NotImplementedException();
        }

        protected abstract KeyValuePair<CommandType, string> ConfigureInsertCommandText();

        public virtual void Insert(TEntity entity) { }

        protected abstract KeyValuePair<CommandType, string> ConfigureUpdateCommandText();

        public virtual void Update(TEntity entityToUpdate) { }

        protected abstract KeyValuePair<CommandType, string> ConfigureDeleteCommandText();

        public virtual void Delete(object id) { }

        public virtual void Delete(TEntity entityToDelete) { }

    }
}
