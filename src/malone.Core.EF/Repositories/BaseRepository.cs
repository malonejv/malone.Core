using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.EF.Entities.Filters;
using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;
using malone.Core.Logging;

namespace malone.Core.EF.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T>, IDisposable
		where T : class
	{
		protected DbContext Context { get; set; }
		protected DbSet<T> EntityDbSet { get; private set; }
		protected ICoreLogger Logger { get; set; }

		#region Constructor

		public BaseRepository(IContext context, ICoreLogger logger)
		{
			Context = context.ThrowIfNull().ThrowIfNotDeriveOfType<DbContext>(nameof(context));
			Logger = logger.ThrowIfNull();
			EntityDbSet = Context.Set<T>();
		}

		#endregion

		#region Public methods

		public virtual IEnumerable<T> GetAll(
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			bool includeDeleted = false,
			string includeProperties = ""
		   )
		{
			ThrowIfDisposed();
			try
			{
				IQueryable<T> query = EntityDbSet;

				query = Get(query, orderBy, includeDeleted, includeProperties);

				return query.ToList();
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(T));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		public virtual IEnumerable<T> Get<TFilter>(
		   TFilter filter = default(TFilter),
		   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
		   bool includeDeleted = false,
		   string includeProperties = "")
			where TFilter : class, IFilterExpression
		{
			ThrowIfDisposed();
			try
			{
				IQueryable<T> query = EntityDbSet;

				Expression<Func<T, bool>> filterExp = null;
				if (filter != null)
				{
					var filterEF = (filter as IFilterExpressionEF<T>);
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

				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(T));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		public virtual T GetEntity<TFilter>(
			TFilter filter = default(TFilter),
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			bool includeDeleted = false,
			string includeProperties = "")
			where TFilter : class, IFilterExpression
		{
			ThrowIfDisposed();
			try
			{
				IQueryable<T> query = EntityDbSet;

				query = Get(query, orderBy, includeDeleted, includeProperties);

				Expression<Func<T, bool>> filterExp = null;
				if (filter != null)
				{
					filterExp = ((IFilterExpressionEF<T>)filter).Expression;
				}

				if (filterExp != null)
				{
					query = query.Where(filterExp);
				}

				return query.FirstOrDefault();
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS601, typeof(T));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		public virtual void Add(T entity)
		{
			ThrowIfDisposed();
			try
			{
					EntityDbSet.Add(entity);
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS602, typeof(T));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		public virtual void Update(T entity)
		{
			ThrowIfDisposed();
			try
			{
				Context.Entry(entity).State = EntityState.Modified;
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS604, typeof(T));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		public virtual void Delete(T entity)
		{
			ThrowIfDisposed();
			try
			{
				EntityDbSet.Remove(entity);
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS603, typeof(T));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		#endregion

		#region Private And Protected Methods

		protected IQueryable<T> Get(
		   IQueryable<T> query,
		   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
		   bool includeDeleted = false,
		   string includeProperties = "")
		{
			try
			{
				if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
				{
					if (!includeDeleted)
					{
						query = ((IQueryable<ISoftDelete>)query)
							.Where(e => e.IsDeleted == includeDeleted)
							.Cast<T>();
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
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(T));
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
