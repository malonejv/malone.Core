using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using malone.Core.AdoNet.Context;
using malone.Core.AdoNet.Entities.Filters;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;

namespace malone.Core.AdoNet.Repositories
{
    public abstract class AdoNetRepository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        private AdoNetContext _context;

        protected AdoNetContext Context => _context;

        protected IUnitOfWork UnitOfWork { get; private set; }
        internal ILogger Logger { get; }


        public AdoNetRepository(IUnitOfWork unitOfWork, ILogger logger)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            UnitOfWork = unitOfWork;
            _context = (AdoNetContext)UnitOfWork.Context;

            Logger = logger;
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
                    //TODO: Implementar solucion
                    //Context.Db.AddParameterIsDeleted(command, includeDeleted);
                }

                IDataAdapter adapter = Context.Db.CreateAdapter(command);
                DataSet ds = new DataSet();

                adapter.Fill(ds);

                command.Dispose();

                List<TEntity> result = new List<TEntity>();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    TEntity entityMapped;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        entityMapped = Map(row);
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
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity));
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        protected KeyValuePair<CommandType, string> validateCommandText(KeyValuePair<CommandType, string> commandTextConfig)
        {
            try
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
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS605, commandTextConfig);
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
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

                    var command = Context.Db.CreateCommand(commandText, type, connection);

                    if (filter != default(TFilter))
                        (filter as IFilterExpressionAdoNet).SetConfiguredParameters(command, Context.Db);

                    query = GetQueryable(command, includeDeleted, orderBy);
                }

                return query.ToList<TEntity>();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
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

                    var command = Context.Db.CreateCommand(commandText, type, connection);

                    query = GetQueryable(command, includeDeleted, orderBy);
                }

                return query.ToList<TEntity>();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        protected abstract KeyValuePair<CommandType, string> ConfigureGetByIdCommandText(bool includeDeleted, string includeProperties);

        public virtual TEntity GetById(
            TKey id,
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

                    var command = Context.Db.CreateCommand(commandText, type, connection);

                    //TODO: Implementar solucion
                    //Context.Db.AddParameterId(command, id);

                    query = GetQueryable(command, includeDeleted);
                }

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
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

        public virtual void Update(TEntity oldValues, TEntity newValues) { }

        protected abstract KeyValuePair<CommandType, string> ConfigureDeleteCommandText();

        public virtual void Delete(TEntity entityToDelete) { }

        protected abstract TEntity Map(DataRow row);

    }


    public abstract class AdoNetRepository<TEntity> : AdoNetRepository<int, TEntity>, IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        public AdoNetRepository(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
        {
        }
    }
}
