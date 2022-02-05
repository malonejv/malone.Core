using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.EF.Entities.Filters;
using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;

namespace malone.Core.EF.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable
        where TEntity : class
    {
        protected DbSet<TEntity> EntityDbSet { get; private set; }
        protected DbContext Context { get; private set; }

        protected ILogger Logger { get; set; }

        #region Constructor

        public BaseRepository(IContext context, ILogger logger)
        {
            CheckContext(context);
            CheckLogger(logger);

            Context = (DbContext)context;
            EntityDbSet = Context.Set<TEntity>();

            Logger = logger;
        }

        #endregion

        #region CRUD Operations

        #region GET ALL

        public virtual IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = ""
           )
        {
            try
            {
                IQueryable<TEntity> query = EntityDbSet;

                query = Get(query, orderBy, includeDeleted, includeProperties);

                return query.ToList();
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

        #endregion

        #region GET FILTERED

        public virtual IEnumerable<TEntity> Get<TFilter>(
           TFilter filter = default(TFilter),
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false,
           string includeProperties = "")
            where TFilter : class, IFilterExpression
        {
            try
            {
                IQueryable<TEntity> query = EntityDbSet;

                Expression<Func<TEntity, bool>> filterExp = null;
                if (filter != null)
                {
                    var filterEF = (filter as IFilterExpressionEF<TEntity>);
                    filterExp = filterEF?.Expression;
                }

                if (filterExp != null)
                {
                    query = query.Where(filterExp);
                }

                query = Get(query, orderBy, includeDeleted, includeProperties);

                return query.ToList();
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

        #endregion

        #region GET ENTITY

        public virtual TEntity GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = "")
            where TFilter : class, IFilterExpression
        {
            try
            {
                IQueryable<TEntity> query = EntityDbSet;

                query = Get(query, orderBy, includeDeleted, includeProperties);

                Expression<Func<TEntity, bool>> filterExp = null;
                if (filter != null)
                {
                    filterExp = ((IFilterExpressionEF<TEntity>)filter).Expression;
                }

                if (filterExp != null)
                {
                    query = query.Where(filterExp);
                }

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS601, typeof(TEntity));
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        #endregion

        #region ADD

        public virtual void Insert(TEntity entity)
        {
            try
            {
                EntityDbSet.Add(entity);
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS602, typeof(TEntity));
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        #endregion

        #region UPDATE

        public virtual void Update(TEntity oldValues, TEntity newValues)
        {
            try
            {
                Context.Entry(oldValues).State = EntityState.Detached;
                Context.Entry(newValues).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS604, typeof(TEntity));
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

        #endregion

        #region DELETE

        public virtual void Delete(TEntity entity)
        {
            try
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    EntityDbSet.Attach(entity);
                }
                EntityDbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS603, typeof(TEntity));
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
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

            if (!(context is DbContext))
            {
                //TODO: Implementar excepciones del core
                throw new ArgumentException();
            }
        }

        protected IQueryable<TEntity> Get(
           IQueryable<TEntity> query,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false,
           string includeProperties = "")
        {
            try
            {
                if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
                {
                    if (!includeDeleted)
                    {
                        query = ((IQueryable<ISoftDelete>)query)
                            .Where(e => e.IsDeleted == includeDeleted)
                            .Cast<TEntity>();
                    }
                }

                if (!string.IsNullOrEmpty(includeProperties))
                {
                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }

                if (orderBy != null)
                {
                    return orderBy(query).AsNoTracking<TEntity>();
                }
                else
                {
                    return query.AsNoTracking<TEntity>();
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
                throw new ObjectDisposedException(GetType().Name);
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
