using malone.Core.AdoNet.Database;
using malone.Core.AdoNet.Entities;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace malone.Core.AdoNet.Repositories
{
    public abstract class Repository<TKey, TEntity> : BaseRepository<TEntity>, IRepository<TKey, TEntity>, IDisposable
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        //protected AdoNetDbContext Context { get; private set; }
        //protected ILogger Logger { get; }


        public Repository(IContext context, ILogger logger) : base(context, logger) { }

        //private void CheckLogger(ILogger logger)
        //{
        //    if (logger == null) throw new ArgumentNullException(nameof(logger));
        //}

        //private void CheckContext(IContext context)
        //{
        //    if (context == null) throw new ArgumentNullException(nameof(context));

        //    if (!(context is AdoNetDbContext))
        //    {
        //        //TODO: Implementar excepciones del core
        //        throw new ArgumentException();
        //    }
        //}

        #region CRUD Operations

        #region GET BY ID

        protected abstract void ConfigureCommandForGetById(IDbCommand command, bool includeDeleted, string includeProperties);

        public virtual TEntity GetById(
            TKey id,
            bool includeDeleted = false,
            string includeProperties = "")
        {
            ThrowIfDisposed();
            try
            {
                IQueryable<TEntity> query;

                var command = Context.CreateCommand();
                ConfigureCommandForGetById(command, includeDeleted, includeProperties);

                List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
                DbParameterWithValue parameter = typeof(TEntity).GetKeyParameter<TKey>(id);
                parameters.Add(parameter);
                Context.AddCommandParameters(command, parameters);

                query = GetQueryable(command, includeDeleted);

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        #endregion

        //#region GET ALL

        //protected abstract void ConfigureCommandForGetAll(IDbCommand command, bool includeDeleted, string includeProperties);

        //public virtual IEnumerable<TEntity> GetAll(
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    bool includeDeleted = false,
        //    string includeProperties = ""
        //   )
        //{
        //    ThrowIfDisposed();
        //    try
        //    {
        //        IQueryable<TEntity> query;

        //        var command = Context.CreateCommand();
        //        ConfigureCommandForGetAll(command, includeDeleted, includeProperties);
        //        query = GetQueryable(command, includeDeleted, orderBy);

        //        return query.ToList<TEntity>();
        //    }
        //    catch (Exception ex)
        //    {
        //        var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
        //        if (Logger != null) Logger.Error(techEx);

        //        throw techEx;
        //    }
        //}

        //#endregion

        //#region GET FILTERED

        //protected abstract void ConfigureCommandForGet(IDbCommand command, bool includeDeleted, string includeProperties);

        //public virtual IEnumerable<TEntity> Get<TFilter>(
        //   TFilter filter = default(TFilter),
        //   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //   bool includeDeleted = false,
        //   string includeProperties = "")
        //    where TFilter : class, IFilterExpression
        //{
        //    ThrowIfDisposed();
        //    try
        //    {
        //        IQueryable<TEntity> query;

        //        var command = Context.CreateCommand();
        //        ConfigureCommandForGet(command, includeDeleted, includeProperties);

        //        IEnumerable<DbParameterWithValue> parameters;

        //        if (filter != default(TFilter) && typeof(IFilterExpressionAdoNet).IsAssignableFrom(typeof(TFilter)))
        //        {
        //            parameters = (filter as IFilterExpressionAdoNet).GetParameters(command);
        //            Context.AddCommandParameters(command, parameters);
        //        }

        //        query = GetQueryable(command, includeDeleted, orderBy);

        //        return query.ToList<TEntity>();
        //    }
        //    catch (Exception ex)
        //    {
        //        var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
        //        if (Logger != null) Logger.Error(techEx);

        //        throw techEx;
        //    }
        //}

        //#endregion

        //#region GET ENTITY

        //protected abstract void ConfigureCommandForGetEntity(IDbCommand command, bool includeDeleted, string includeProperties);

        //public virtual TEntity GetEntity<TFilter>(
        //    TFilter filter = default(TFilter),
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    bool includeDeleted = false,
        //    string includeProperties = "")
        //    where TFilter : class, IFilterExpression
        //{
        //    ThrowIfDisposed();
        //    try
        //    {
        //        IQueryable<TEntity> query;

        //        var command = Context.CreateCommand();
        //        ConfigureCommandForGetEntity(command, includeDeleted, includeProperties);

        //        IEnumerable<DbParameterWithValue> parameters;

        //        if (filter != default(TFilter) && typeof(IFilterExpressionAdoNet).IsAssignableFrom(typeof(TFilter)))
        //        {
        //            parameters = (filter as IFilterExpressionAdoNet).GetParameters(command);
        //            Context.AddCommandParameters(command, parameters);
        //        }

        //        query = GetQueryable(command, includeDeleted, orderBy);

        //        return query.SingleOrDefault<TEntity>();
        //    }
        //    catch (Exception ex)
        //    {
        //        var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
        //        if (Logger != null) Logger.Error(techEx);

        //        throw techEx;
        //    }
        //}

        //#endregion

        //#region ADD

        //protected abstract void ConfigureCommandForInsert(IDbCommand command);

        //public virtual void Insert(TEntity entity)
        //{
        //    ThrowIfDisposed();
        //    try
        //    {
        //        var command = Context.CreateCommand();
        //        ConfigureCommandForInsert(command);

        //        IEnumerable<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
        //        parameters = entity.GetNotKeyParameters<TKey, TEntity>();
        //        Context.AddCommandParameters(command, parameters);

        //        command.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS602, typeof(TEntity));
        //        if (Logger != null) Logger.Error(techEx);

        //        throw techEx;
        //    }
        //}

        //#endregion

        #region UPDATE

        protected virtual List<DbParameterWithValue> GetUpdateParameters(List<DbParameterWithValue> parameters, TEntity entity)
        {
            DbParameterWithValue parameter = typeof(TEntity).GetKeyParameter<TKey>(entity.Id);
            parameters.Add(parameter);

            parameters.AddRange(entity.GetNotKeyParameters<TKey, TEntity>().ToList());

            return parameters;
        }

        public virtual void Update(TEntity entity)
        {
            ThrowIfDisposed();
            try
            {
                var command = Context.CreateCommand();
                ConfigureCommandForUpdate(command);

                List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
                parameters = GetUpdateParameters(parameters, entity);
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

        //protected abstract void ConfigureCommandForDelete(IDbCommand command);

        //public virtual void Delete(TEntity entity)
        //{
        //    ThrowIfDisposed();
        //    try
        //    {
        //        var command = Context.CreateCommand();
        //        ConfigureCommandForDelete(command);

        //        List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
        //        DbParameterWithValue parameter = typeof(TEntity).GetKeyParameter<TKey>(entity.Id);
        //        parameters.Add(parameter);
        //        Context.AddCommandParameters(command, parameters);

        //        command.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS603, typeof(TEntity));
        //        if (Logger != null) Logger.Error(techEx);

        //        throw techEx;
        //    }
        //}

        protected override List<DbParameterWithValue> GetDeleteParameters(List<DbParameterWithValue> parameters, TEntity entity)
        {
            DbParameterWithValue parameter = typeof(TEntity).GetKeyParameter<TKey>(entity.Id);
            parameters.Add(parameter);
            return parameters;
        }

        #endregion

        #endregion

        //#region Public And Protected Methods

        //protected IQueryable<TEntity> GetQueryable(
        //   IDbCommand command,
        //   bool includeDeleted = false,
        //   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        //{
        //    try
        //    {

        //        if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
        //        {
        //            //TODO: Implementar solucion
        //            //Context.Db.AddParameterIsDeleted(command, includeDeleted);
        //        }

        //        IDataAdapter adapter = Context.CreateAdapter(command);
        //        DataSet ds = new DataSet();

        //        adapter.Fill(ds);

        //        command.Dispose();

        //        List<TEntity> result = new List<TEntity>();
        //        if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
        //        {
        //            TEntity entityMapped;
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                entityMapped = Map(row);
        //                result.Add(entityMapped);
        //            }
        //        }

        //        var query = result.AsQueryable<TEntity>();
        //        if (orderBy != null && result.Count > 0)
        //        {
        //            return orderBy(query);
        //        }
        //        else
        //        {
        //            return query;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity));
        //        if (Logger != null) Logger.Error(techEx);

        //        throw techEx;
        //    }
        //}

        //protected abstract TEntity Map(DataRow row);

        //#endregion

        //#region Dispose

        //protected bool _disposed;

        ///// <summary>
        /////     Dispose the store
        ///// </summary>
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected void ThrowIfDisposed()
        //{
        //    if (_disposed)
        //    {
        //        throw new ObjectDisposedException(GetType().Name);
        //    }
        //}

        ///// <summary>
        /////     If disposing, calls dispose on the Context.  Always nulls out the Context
        ///// </summary>
        ///// <param name="disposing"></param>
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposing && _context != null)
        //    {
        //        _context.Dispose();
        //    }
        //    _disposed = true;
        //    _context = null;
        //}

        //#endregion
    }


    public abstract class Repository<TEntity> : Repository<int, TEntity>, IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        public Repository(IContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
