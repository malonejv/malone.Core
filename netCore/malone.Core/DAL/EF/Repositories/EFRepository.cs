﻿using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler.Interfaces;
using malone.Core.CL.Exceptions.Manager.Interfaces;
using malone.Core.DAL.Base.Repositories;
using malone.Core.DAL.Base.UnitOfWork;
using malone.Core.DAL.EF.Context;
using malone.Core.EL;
using malone.Core.EL.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace malone.Core.DAL.EF.Repositories
{
    public class EFRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        protected DbSet<TEntity> _dbSet;
        protected EFDbContext _context;

        protected IUnitOfWork UnitOfWork { get; }

        protected IExceptionMessageManager MessageManager { get; }

        protected IExceptionHandler ExceptionHandler { get; }

        public EFRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            UnitOfWork = unitOfWork;
            _context = (EFDbContext)UnitOfWork.Context;
            _dbSet = _context.Set<TEntity>();
        }

        public EFRepository(IUnitOfWork unitOfWork, IExceptionMessageManager exManager, IExceptionHandler exHandler)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            UnitOfWork = unitOfWork;
            _context = (EFDbContext)UnitOfWork.Context;
            _dbSet = _context.Set<TEntity>();

            MessageManager = exManager;
            ExceptionHandler = exHandler;
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
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E400), typeof(TEntity));
                var dex = new DataAccessException((int)CoreErrors.E400, message, ex);
                ExceptionHandler.HandleException(dex);
            }
            return null;
        }

        public virtual IEnumerable<TEntity> Get<TFilter>(
           TFilter filter = default(TFilter),
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false,
           string includeProperties = "")
            where TFilter : class, IFilter
        {

            try
            {
                IQueryable<TEntity> query = _dbSet;

                Expression<Func<TEntity, bool>> filterExp = null;
                if (filter != null)
                {
                    filterExp = (filter as IFilterEF<TEntity>).Expression;
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
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E400), typeof(TEntity));
                var dex = new DataAccessException((int)CoreErrors.E400, message, ex);
                ExceptionHandler.HandleException(dex);
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
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E400), typeof(TEntity));
                var dex = new DataAccessException((int)CoreErrors.E400, message, ex);
                ExceptionHandler.HandleException(dex);
            }
            return null;
        }

        public virtual TEntity GetById(
            object id,
            bool includeDeleted = false,
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

                query = Get(query, null, includeDeleted, includeProperties);

                return query.Where<TEntity>(e => e.Id == (int)id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E401), typeof(TEntity));
                var dex = new DataAccessException((int)CoreErrors.E401, message, ex);
                ExceptionHandler.HandleException(dex);
            }
            return null;
        }

        public virtual TEntity GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = "")
            where TFilter : class, IFilter
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

                query = Get(query, orderBy, includeDeleted, includeProperties);

                Expression<Func<TEntity, bool>> filterExp = null;
                if (filter != null)
                {
                    filterExp = ((IFilterEF<TEntity>)filter).Expression;
                }

                if (filterExp != null)
                {
                    query = query.Where(filterExp);
                }

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E401), typeof(TEntity));
                var dex = new DataAccessException((int)CoreErrors.E401, message, ex);
                ExceptionHandler.HandleException(dex);
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
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E402), typeof(TEntity));
                var dex = new DataAccessException((int)CoreErrors.E402, message, ex);
                ExceptionHandler.HandleException(dex);
            }
        }

        public virtual void Delete(object id)
        {
            try
            {
                TEntity entityToDelete = _dbSet.Find(id);
                Delete(entityToDelete);
            }
            catch (Exception ex)
            {
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E403), typeof(TEntity));
                var dex = new DataAccessException((int)CoreErrors.E403, message, ex);
                ExceptionHandler.HandleException(dex);
            }
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            try
            {
                _dbSet.Attach(entityToUpdate);
                _context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E404), typeof(TEntity));
                var dex = new DataAccessException((int)CoreErrors.E404, message, ex);
                ExceptionHandler.HandleException(dex);
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
                var message = string.Format(MessageManager.GetDescription((int)CoreErrors.E403), typeof(TEntity));
                var dex = new DataAccessException((int)CoreErrors.E403, message, ex);
                ExceptionHandler.HandleException(dex);
            }
        }

    }
}
