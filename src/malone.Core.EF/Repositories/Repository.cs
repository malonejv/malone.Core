using malone.Core.Commons.DI;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.EF.Entities;
using malone.Core.EF.Entities.Filters;
using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace malone.Core.EF.Repositories.Implementations
{
    public class Repository<TKey, TEntity> : BaseRepository<TEntity>, IRepository<TKey, TEntity>, IDisposable
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        #region Constructor

        public Repository(IContext context, ILogger logger) : base(context, logger) { }

        //private void CheckLogger(ILogger logger)
        //{
        //    if (logger == null) throw new ArgumentNullException(nameof(logger));
        //}

        //private void CheckContext(IContext context)
        //{
        //    if (context == null) throw new ArgumentNullException(nameof(context));

        //    if (!(context is DbContext))
        //    {
        //        //TODO: Implementar excepciones del core
        //        throw new ArgumentException();
        //    }
        //}

        #endregion

        #region CRUD Operations

        #region GET ALL

        public virtual TEntity GetById(
            TKey id,
            bool includeDeleted = false,
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = EntityDbSet;

                query = Get(query, null, includeDeleted, includeProperties);

                return query.Where<TEntity>(e => e.Id.Equals(id)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS601, typeof(TEntity));
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        #endregion

        //#region GET ALL

        //public virtual IEnumerable<TEntity> GetAll(
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    bool includeDeleted = false,
        //    string includeProperties = ""
        //   )
        //{
        //    try
        //    {
        //        IQueryable<TEntity> query = _dbSet;

        //        query = Get(query, orderBy, includeDeleted, includeProperties);

        //        return query.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity));
        //        if (Logger != null) Logger.Error(techEx);

        //        throw techEx;
        //    }
        //}

        //#endregion

        //#region GET FILTERED

        //public virtual IEnumerable<TEntity> Get<TFilter>(
        //   TFilter filter = default(TFilter),
        //   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //   bool includeDeleted = false,
        //   string includeProperties = "")
        //    where TFilter : class, IFilterExpression
        //{
        //    try
        //    {
        //        IQueryable<TEntity> query = _dbSet;

        //        Expression<Func<TEntity, bool>> filterExp = null;
        //        if (filter != null)
        //        {
        //            var filterEF = (filter as IFilterExpressionEF<TEntity>);
        //            filterExp = filterEF?.Expression;
        //        }

        //        if (filterExp != null)
        //        {
        //            query = query.Where(filterExp);
        //        }

        //        query = Get(query, orderBy, includeDeleted, includeProperties);

        //        return query.ToList();
        //    }
        //    catch (Exception ex)
        //    {

        //        var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity));
        //        if (Logger != null) Logger.Error(techEx);

        //        throw techEx;
        //    }
        //}

        //#endregion

        //#region GET ENTITY

        //public virtual TEntity GetEntity<TFilter>(
        //    TFilter filter = default(TFilter),
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    bool includeDeleted = false,
        //    string includeProperties = "")
        //    where TFilter : class, IFilterExpression
        //{
        //    try
        //    {
        //        IQueryable<TEntity> query = _dbSet;

        //        query = Get(query, orderBy, includeDeleted, includeProperties);

        //        Expression<Func<TEntity, bool>> filterExp = null;
        //        if (filter != null)
        //        {
        //            filterExp = ((IFilterExpressionEF<TEntity>)filter).Expression;
        //        }

        //        if (filterExp != null)
        //        {
        //            query = query.Where(filterExp);
        //        }

        //        return query.FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS601, typeof(TEntity));
        //        if (Logger != null) Logger.Error(techEx);

        //        throw techEx;
        //    }
        //}

        //#endregion

        //#region ADD

        //public virtual void Insert(TEntity entity)
        //{
        //    try
        //    {
        //        _dbSet.Add(entity);
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

        public virtual void Update(TEntity entity)
        {
            try
            {
                var oldValues = GetById(entity.Id);

                if (oldValues.Equals(default(TEntity)))
                    throw CoreExceptionFactory.CreateException<TechnicalException>(CoreErrors.DATAACCESS601, typeof(TEntity));

                Context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS604, typeof(TEntity));
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

        //public virtual void Update(TEntity oldValues, TEntity newValues)
        //{
        //    try
        //    {
        //        _context.Entry(oldValues).State = EntityState.Detached;

        //        _context.Entry(newValues).State = EntityState.Modified;
        //    }
        //    catch (Exception ex)
        //    {
        //        var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS604, typeof(TEntity));
        //        if (Logger != null) Logger.Error(techEx);

        //        throw techEx;
        //    }
        //}

        #endregion

        //#region DELETE

        //public virtual void Delete(TEntity entity)
        //{
        //    try
        //    {
        //        if (_context.Entry(entity).State == EntityState.Detached)
        //        {
        //            _dbSet.Attach(entity);
        //        }
        //        _dbSet.Remove(entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS603, typeof(TEntity));
        //        if (Logger != null) Logger.Error(techEx);

        //        throw techEx;
        //    }
        //}

        //#endregion

        //#endregion

        #region Private And Protected Methods

        public void SetAddOrUpdate<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : IBaseEntity
        {
            foreach (var entity in entities)
            {
                Context.Entry(entity).State = entity.AddOrUpdate();
            }
        }

        //protected IQueryable<TEntity> Get(
        //   IQueryable<TEntity> query,
        //   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //   bool includeDeleted = false,
        //   string includeProperties = "")
        //{
        //    try
        //    {
        //        if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
        //        {
        //            if (!includeDeleted)
        //            {
        //                query = ((IQueryable<ISoftDelete>)query)
        //                    .Where(e => e.IsDeleted == includeDeleted)
        //                    .Cast<TEntity>();
        //            }
        //        }

        //        if (!string.IsNullOrEmpty(includeProperties))
        //        {
        //            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //            {
        //                query = query.Include(includeProperty);
        //            }
        //        }

        //        if (orderBy != null)
        //        {
        //            return orderBy(query).AsNoTracking<TEntity>();
        //        }
        //        else
        //        {
        //            return query.AsNoTracking<TEntity>();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity));
        //        if (Logger != null) Logger.Error(techEx);

        //        throw techEx;
        //    }
        //}

        #endregion

        //#region Dispose

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

        #endregion

    }


    public class Repository<TEntity> :
        Repository<int, TEntity>,
        IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        public Repository(IContext context, ILogger logger) : base(context, logger)
        {
        }
    }

}

