using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using malone.Core.AdoNet.Context;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.Dapper.Attributes;
using malone.Core.Dapper.Entities;
using malone.Core.Dapper.Entities.Filters;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;

namespace malone.Core.Dapper.Repositories
{

    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable
        where TEntity : class
    {
        protected CoreDbContext Context { get; private set; }
        protected IDbConnection Connection => Context.Connection;
        protected ILogger Logger { get; }

        protected Type TEntityType { get; set; }

        #region Constructor

        public BaseRepository(IContext context, ILogger logger)
        {
            CheckContext(context);
            CheckLogger(logger);

            Context = (CoreDbContext)context;
            Logger = logger;
            TEntityType = this.GetType().GetInterface("IBaseRepository`1").GetGenericArguments()[0];
        }

        #endregion

        #region CRUD Operations

        #region GET ALL

        protected virtual CommandDefinition ConfigureCommandForGetAll(bool includeDeleted, string includeProperties)
        {
            string tableName = TEntityType.GetTableName();
            string columns = TEntityType.GetColumnNames();
            string query = string.Format("SELECT {0} FROM {1}", columns, tableName);

            DynamicParameters parameters = new DynamicParameters();
            var allowSoftDelete = ConfigureParameterIsDelete(query, columns, tableName, parameters, includeDeleted);
            query += ";";

            return new CommandDefinition(query, transaction: Context.Transaction, commandType: CommandType.Text, parameters: parameters);
        }

        public virtual IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = ""
           )
        {
            ThrowIfDisposed();
            try
            {
                IQueryable<TEntity> queryableResult;

                var command = ConfigureCommandForGetAll(includeDeleted, includeProperties);
                queryableResult = GetQueryable(command, orderBy);

                return queryableResult.ToList<TEntity>();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        #endregion

        #region GET FILTERED

        protected virtual CommandDefinition ConfigureCommandForGet<TFilter>(TFilter filter, bool includeDeleted, string includeProperties) where TFilter : class, IFilterExpression
        {
            string tableName = TEntityType.GetTableName();
            string columns = TEntityType.GetColumnNames();
            string query = string.Format("SELECT {0} FROM {1}", columns, tableName);

            DynamicParameters parameters = new DynamicParameters();

            var whereClause = " WHERE ";
            List<ParameterAttribute> parametersInfo;
            if (filter != default(TFilter) && typeof(IFilterExpressionDapper).IsAssignableFrom(typeof(TFilter)))
            {
                parametersInfo = (filter as IFilterExpressionDapper).GetParameters();
                foreach (var parameterInfo in parametersInfo)
                {
                    int? size = parameterInfo.IsSizeDefined ? new int?(parameterInfo.Size) : null;
                    parameters.Add(parameterInfo.Name, parameterInfo.Value, parameterInfo.Type, parameterInfo.Direction, size);
                }

                int i = 0;
                var ColumnNames = columns.Split(',').Select(c => c.Trim()).ToList();
                foreach (var parameterName in parameters.ParameterNames)
                {
                    var exists = ColumnNames.Contains(parameterName);
                    if (exists)
                    {
                        if (i == 0)
                        {
                            whereClause += $"{parameterName} = @{parameterName}";
                        }
                        else
                        {
                            whereClause += $" AND {parameterName} = @{parameterName}";
                        }
                    }
                    else
                    {
                        if (parameterName.Contains("Or"))
                        {
                            var options = parameterName.Split(new String[] { "Or" }, StringSplitOptions.RemoveEmptyEntries);
                            if (options.Count() > 1)
                            {
                                int j = 0;
                                string optionsQuery = "(";
                                if (i != 0)
                                {
                                    optionsQuery = "AND (";
                                }

                                foreach (var option in options)
                                {
                                    exists = ColumnNames.Contains(option);
                                    if (exists)
                                    {
                                        if (j == 0)
                                        {
                                            optionsQuery += $"{option} = @{parameterName}";
                                        }
                                        else
                                        {
                                            optionsQuery += $" OR {option} = @{parameterName}";
                                        }
                                    }
                                    j++;
                                }
                                optionsQuery += ")";
                                whereClause += optionsQuery;
                            }
                        }
                    }

                    i++;
                }
            }
            else
            {
                //TODO: Usar errores del core
                throw new Exception("Filter must implement IFilterExpressionDapper.");
            }

            query += $"{whereClause};";

            return new CommandDefinition(query, transaction: Context.Transaction, commandType: CommandType.Text, parameters: parameters);
        }

        public virtual IEnumerable<TEntity> Get<TFilter>(
           TFilter filter = default(TFilter),
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false,
           string includeProperties = "")
            where TFilter : class, IFilterExpression
        {
            ThrowIfDisposed();
            try
            {
                IQueryable<TEntity> query;

                var command = ConfigureCommandForGet(filter, includeDeleted, includeProperties);
                query = GetQueryable(command, orderBy);

                return query.ToList<TEntity>();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, TEntityType.Name);
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        #endregion

        #region GET ENTITY

        protected virtual CommandDefinition ConfigureCommandForGetEntity<TFilter>(TFilter filter, bool includeDeleted, string includeProperties) where TFilter : class, IFilterExpression
        {
            return ConfigureCommandForGet(filter, includeDeleted, includeProperties);
        }

        public virtual TEntity GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = "")
            where TFilter : class, IFilterExpression
        {
            ThrowIfDisposed();
            try
            {
                IQueryable<TEntity> query;

                var command = ConfigureCommandForGet(filter, includeDeleted, includeProperties);
                query = GetQueryable(command, orderBy);

                return query.SingleOrDefault<TEntity>();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, TEntityType.Name);
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        #endregion

        #region ADD

        //protected abstract void ConfigureCommandForInsert(IDbCommand command);

        //protected virtual List<DbParameterWithValue> GetInsertParameters(List<DbParameterWithValue> parameters, TEntity entity)
        //{
        //    parameters = entity.GetParameters().ToList();
        //    return parameters;
        //}

        public virtual void Insert(TEntity entity)
        {
            //ThrowIfDisposed();
            //try
            //{
            //    var command = Context.CreateCommand();
            //    ConfigureCommandForInsert(command);

            //    List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
            //    parameters = GetInsertParameters(parameters, entity);
            //    Context.AddCommandParameters(command, parameters);

            //    command.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS602, TEntityType);
            //    if (Logger != null) Logger.Error(techEx);

            //    throw techEx;
            //}
        }

        #endregion

        #region UPDATE

        //protected abstract void ConfigureCommandForUpdate(IDbCommand command);

        //protected virtual List<DbParameterWithValue> GetUpdateParameters(List<DbParameterWithValue> parameters, TEntity oldValues, TEntity newValues)
        //{
        //    parameters = newValues.GetParameters().ToList();
        //    return parameters;
        //}

        public virtual void Update(TEntity oldValues, TEntity newValues)
        {
            //ThrowIfDisposed();
            //try
            //{
            //    var command = Context.CreateCommand();
            //    ConfigureCommandForUpdate(command);

            //    List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
            //    parameters = GetUpdateParameters(parameters, oldValues, newValues);
            //    Context.AddCommandParameters(command, parameters);

            //    command.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS604, TEntityType);
            //    if (Logger != null) Logger.Error(techEx);

            //    throw techEx;
            //}
        }

        #endregion

        #region DELETE

        //protected abstract void ConfigureCommandForDelete(IDbCommand command);

        //protected virtual List<DbParameterWithValue> GetDeleteParameters(List<DbParameterWithValue> parameters, TEntity entity)
        //{
        //    parameters = entity.GetParameters().ToList();
        //    return parameters;
        //}

        public virtual void Delete(TEntity entityToDelete)
        {
            //ThrowIfDisposed();
            //try
            //{
            //    var command = Context.CreateCommand();
            //    ConfigureCommandForDelete(command);

            //    List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
            //    parameters = GetDeleteParameters(parameters, entityToDelete);
            //    Context.AddCommandParameters(command, parameters);

            //    command.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS603, typeof(TEntity));
            //    if (Logger != null) Logger.Error(techEx);

            //    throw techEx;
            //}
        }

        #endregion

        #endregion

        #region Private And Protected Methods

        private void CheckLogger(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
        }

        private void CheckContext(IContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!(context is CoreDbContext))
            {
                //TODO: Implementar excepciones del core
                throw new ArgumentException();
            }
        }

        protected bool ConfigureParameterIsDelete(string query, string columns, string tableName, DynamicParameters parameters, bool includeDeleted)
        {
            var allowSoftDelete = typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity));
            if (allowSoftDelete)
            {
                columns.Concat(", IsDeleted");
                query = string.Format("SELECT {0} FROM {1}", columns, tableName);

                query += " WHERE IsDeleted = @IsDeleted";
            }
            return allowSoftDelete;
        }

        protected IQueryable<TEntity> GetQueryable(
           CommandDefinition commandDefinition,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            try
            {
                List<TEntity> result = Connection.Query<TEntity>(commandDefinition).ToList();

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
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        protected abstract TEntity Map(DataRow row);

        #endregion

        #region Dispose

        protected bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && Context != null)
            {
                Context.Dispose();
            }
            _disposed = true;
            Context = null;
        }

        #endregion
    }
}
