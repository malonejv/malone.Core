using malone.Core.AdoNet.Context;
using malone.Core.AdoNet.Database;
using malone.Core.AdoNet.Entities;
using malone.Core.AdoNet.Entities.Filters;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace malone.Core.AdoNet.Repositories
{

    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable
        where TEntity : class
    {
        protected CoreDbContext Context { get; private set; }
        protected ILogger Logger { get; }

        #region Constructor

        public BaseRepository(IContext context, ILogger logger)
        {
            CheckContext(context);
            CheckLogger(logger);

            Context = (CoreDbContext)context;
            Logger = logger;
        }

        #endregion

        #region CRUD Operations

        #region GET ALL

        protected abstract void ConfigureCommandForGetAll(IDbCommand command, bool includeDeleted, string includeProperties);

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

                var command = Context.CreateCommand();
                ConfigureCommandForGetAll(command, includeDeleted, includeProperties);
                queryableResult = GetQueryable(command, includeDeleted, orderBy);

                return queryableResult.ToList<TEntity>();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        #endregion

        #region GET FILTERED

        protected abstract void ConfigureCommandForGet(IDbCommand command, bool includeDeleted, string includeProperties);

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
                IQueryable<TEntity> queryableResult;

                var command = Context.CreateCommand();
                ConfigureCommandForGet(command, includeDeleted, includeProperties);

                IEnumerable<DbParameterWithValue> parameters;

                if (filter != default(TFilter) && typeof(IFilterExpressionAdoNet).IsAssignableFrom(typeof(TFilter)))
                {
                    parameters = (filter as IFilterExpressionAdoNet).GetParameters(command);
                    Context.AddCommandParameters(command, parameters);
                }

                queryableResult = GetQueryable(command, includeDeleted, orderBy);

                return queryableResult.ToList<TEntity>();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        #endregion

        #region GET ENTITY

        protected abstract void ConfigureCommandForGetEntity(IDbCommand command, bool includeDeleted, string includeProperties);

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
                IQueryable<TEntity> queryableResult;

                var command = Context.CreateCommand();
                ConfigureCommandForGetEntity(command, includeDeleted, includeProperties);

                IEnumerable<DbParameterWithValue> parameters;

                if (filter != default(TFilter) && typeof(IFilterExpressionAdoNet).IsAssignableFrom(typeof(TFilter)))
                {
                    parameters = (filter as IFilterExpressionAdoNet).GetParameters(command);
                    Context.AddCommandParameters(command, parameters);
                }

                queryableResult = GetQueryable(command, includeDeleted, orderBy);

                return queryableResult.SingleOrDefault<TEntity>();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        #endregion

        #region ADD

        protected abstract void ConfigureCommandForInsert(IDbCommand command);

        protected virtual List<DbParameterWithValue> GetInsertParameters(List<DbParameterWithValue> parameters, TEntity entity)
        {
            parameters = entity.GetParameters().ToList();
            return parameters;
        }

        public virtual void Insert(TEntity entity)
        {
            ThrowIfDisposed();
            try
            {
                var command = Context.CreateCommand();
                ConfigureCommandForInsert(command);

                List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
                parameters = GetInsertParameters(parameters, entity);
                Context.AddCommandParameters(command, parameters);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS602, typeof(TEntity));
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        #endregion

        #region UPDATE

        protected abstract void ConfigureCommandForUpdate(IDbCommand command);

        protected virtual List<DbParameterWithValue> GetUpdateParameters(List<DbParameterWithValue> parameters, TEntity oldValues, TEntity newValues)
        {
            parameters = newValues.GetParameters().ToList();
            return parameters;
        }

        public virtual void Update(TEntity oldValues, TEntity newValues)
        {
            ThrowIfDisposed();
            try
            {
                var command = Context.CreateCommand();
                ConfigureCommandForUpdate(command);

                List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
                parameters = GetUpdateParameters(parameters, oldValues, newValues);
                Context.AddCommandParameters(command, parameters);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS604, typeof(TEntity));
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        #endregion

        #region DELETE

        protected abstract void ConfigureCommandForDelete(IDbCommand command);

        protected virtual List<DbParameterWithValue> GetDeleteParameters(List<DbParameterWithValue> parameters, TEntity entity)
        {
            parameters = entity.GetParameters().ToList();
            return parameters;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            ThrowIfDisposed();
            try
            {
                var command = Context.CreateCommand();
                ConfigureCommandForDelete(command);

                List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
                parameters = GetDeleteParameters(parameters, entityToDelete);
                Context.AddCommandParameters(command, parameters);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS603, typeof(TEntity));
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        #endregion

        #endregion

        #region Private And Protected Methods

        private void CheckLogger(ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
        }

        private void CheckContext(IContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (!(context is CoreDbContext))
            {
                //TODO: Implementar excepciones del core
                throw new ArgumentException();
            }
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

                IDataAdapter adapter = Context.CreateAdapter(command);
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

                var queryableResult = result.AsQueryable<TEntity>();
                if (orderBy != null && result.Count > 0)
                {
                    return orderBy(queryableResult);
                }
                else
                {
                    return queryableResult;
                }

            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity));
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        protected abstract TEntity Map(DataRow row);

        #endregion

        #region Dispose

        protected bool _disposed;

        /// <summary>
        ///     Dispose the store
        /// </summary>
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

        /// <summary>
        ///     If disposing, calls dispose on the Context.  Always nulls out the Context
        /// </summary>
        /// <param name="disposing"></param>
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
