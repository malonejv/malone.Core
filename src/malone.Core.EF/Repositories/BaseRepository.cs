using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Filters;
using malone.Core.Logging;

namespace malone.Core.EF.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T>, IDisposable
		where T : class
	{
		protected DbContext Context { get; set; }
		protected DbSet<T> EntityDbSet { get; private set; }
		protected ICoreLogger Logger { get; set; }
		protected IBaseQueryRepository<T> QueryRepository { get; private set; }
		protected IBaseCUDRepository<T> CUDRepository { get; private set; }

		#region Constructor & Initializer

		public BaseRepository(IContext context, ICoreLogger logger)
		{
			Context = context.ThrowIfNull().ThrowIfNotOfType<DbContext>();
			Logger = logger.ThrowIfNull();
			EntityDbSet = Context.Set<T>();
			InitializeRepositories(context,logger);
		}

		protected virtual void InitializeRepositories(IContext context, ICoreLogger logger)
		{
			QueryRepository = new BaseQueryRepository<T>(context, logger);
			CUDRepository = new BaseCUDRepository<T>(context, logger);
		}

		#endregion

		#region Public methods

		public IEnumerable<T> Get<TFilter>(TFilter filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, bool includeDeleted, string includeProperties)
			where TFilter : class, IFilterExpression
		{
			ThrowIfDisposed();
			return QueryRepository.Get(filter, orderBy, includeDeleted, includeProperties);
		}

		public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool includeDeleted = false, string includeProperties = "")
		{
			ThrowIfDisposed();
			return QueryRepository.GetAll(orderBy, includeDeleted, includeProperties);
		}

		public T GetEntity<TFilter>(TFilter filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, bool includeDeleted, string includeProperties)
			where TFilter : class, IFilterExpression
		{
			ThrowIfDisposed();
			return QueryRepository.GetEntity(filter, orderBy, includeDeleted, includeProperties);
		}

		public void Insert(T entity)
		{
			ThrowIfDisposed();
			CUDRepository.Insert(entity);
		}

		public void Delete(T entity)
		{
			ThrowIfDisposed();
			CUDRepository.Delete(entity);
		}

		public void Update(T oldValues, T newValues)
		{
			ThrowIfDisposed();
			CUDRepository.Update(oldValues, newValues);
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
