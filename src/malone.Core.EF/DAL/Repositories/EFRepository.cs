using malone.Core.CL.DI;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.DAL.Repositories;
using malone.Core.DAL.UnitOfWork;
using malone.Core.EF.DAL.Exceptions;
using malone.Core.EF.EL.Filters;
using malone.Core.EL.Filters;
using malone.Core.EL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace malone.Core.EF.DAL.Repositories.Implementations
{
    public class EFRepository<TKey, TEntity, TErrorCoder> : ICoreRepository<TKey, TEntity, TErrorCoder>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
        where TErrorCoder : Enum
    {
        protected DbSet<TEntity> _dbSet;
        protected DbContext _context;

        protected IUnitOfWork UnitOfWork { get; }

        protected IExceptionHandler<TErrorCoder> ExceptionHandler { get; }

        internal IEFExceptionHandler EFExceptionHandler { get; }

        public EFRepository(IUnitOfWork unitOfWork, IExceptionHandler<TErrorCoder> exceptionHandler)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
            if (exceptionHandler == null) throw new ArgumentNullException(nameof(exceptionHandler));

            UnitOfWork = unitOfWork;
            _context = UnitOfWork.Context as DbContext;
            _dbSet = _context.Set<TEntity>();

            ExceptionHandler = exceptionHandler;
            EFExceptionHandler = ServiceLocator.Current.Get<IEFExceptionHandler>();
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
                    return orderBy(query);
                }
                else
                {
                    return query;
                }
            }
            catch (Exception ex)
            {
                EFExceptionHandler.HandleException<DataAccessException<EFErrors>>(ex, EFErrors.E600, typeof(TEntity));
            }
            return null;
        }

        public virtual IEnumerable<TEntity> Get<TFilter>(
           TFilter filter = default(TFilter),
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false,
           string includeProperties = "")
            where TFilter : class, IFilterExpression
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

                Expression<Func<TEntity, bool>> filterExp = null;
                if (filter != null)
                {
                    filterExp = (filter as IFilterExpressionEF<TEntity>).Expression;
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
                EFExceptionHandler.HandleException<DataAccessException<EFErrors>>(ex, EFErrors.E600, typeof(TEntity));
            }
            return null;
        }

        public virtual IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = ""
           )
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

                query = Get(query, orderBy, includeDeleted, includeProperties);

                return query.ToList();
            }
            catch (Exception ex)
            {
                EFExceptionHandler.HandleException<DataAccessException<EFErrors>>(ex, EFErrors.E600, typeof(TEntity));
            }
            return null;
        }

        public virtual TEntity GetById(
            TKey id,
            bool includeDeleted = false,
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

                query = Get(query, null, includeDeleted, includeProperties);

                return query.Where<TEntity>(e => e.Id.Equals(id)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                EFExceptionHandler.HandleException<DataAccessException<EFErrors>>(ex, EFErrors.E601, typeof(TEntity));
            }
            return null;
        }

        public virtual TEntity GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = "")
            where TFilter : class, IFilterExpression
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

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
                EFExceptionHandler.HandleException<DataAccessException<EFErrors>>(ex, EFErrors.E601, typeof(TEntity));
            }
            return null;
        }

        public virtual void Insert(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                EFExceptionHandler.HandleException<DataAccessException<EFErrors>>(ex, EFErrors.E602, typeof(TEntity));
            }
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            try
            {
                var old = GetById(entityToUpdate.Id);
                _context.Entry(old).State = EntityState.Detached;

                _context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                EFExceptionHandler.HandleException<DataAccessException<EFErrors>>(ex, EFErrors.E604, typeof(TEntity));
            }
        }

        public virtual void Delete(TKey id)
        {
            try
            {
                TEntity entityToDelete = _dbSet.Find(id);
                Delete(entityToDelete);
            }
            catch (Exception ex)
            {
                EFExceptionHandler.HandleException<DataAccessException<EFErrors>>(ex, EFErrors.E603, typeof(TEntity));
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            try
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
                _dbSet.Remove(entityToDelete);
            }
            catch (Exception ex)
            {
                EFExceptionHandler.HandleException<DataAccessException<EFErrors>>(ex, EFErrors.E603, typeof(TEntity));
            }
        }

    }


    public class EFRepository<TEntity, TErrorCoder> : 
        EFRepository<int, TEntity, TErrorCoder>, 
        ICoreRepository<TEntity, TErrorCoder>
        where TEntity : class, IBaseEntity
        where TErrorCoder : Enum
    {

        //public EFRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        //{
        //}

        public EFRepository(IUnitOfWork unitOfWork, IExceptionHandler<TErrorCoder> exceptionHandler) : base(unitOfWork, exceptionHandler)
        {
        }

    }

}
