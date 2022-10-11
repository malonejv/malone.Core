using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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
		protected DbSet<T> EntityDbSet { get; private set; }
		protected DbContext Context { get; private set; }
		protected ICoreLogger Logger { get; set; }
		public IBaseQueryRepository<T> QueryRepository { get; }
		public IBaseDataManipulationRepository<T> DataManipulationRepository { get; }

		#region Constructor

		public BaseRepository(IContext context, ICoreLogger logger, IBaseQueryRepository<T> queryRepository, IBaseDataManipulationRepository<T> dataManipulationRepository)
		{
			Context = context.ThrowIfNull().ThrowIfNotOfType<IContext, DbContext>();
			Logger = logger.ThrowIfNull();
			QueryRepository = queryRepository.ThrowIfNull();
			DataManipulationRepository = dataManipulationRepository.ThrowIfNull();
			EntityDbSet = Context.Set<T>();
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
			DataManipulationRepository.Insert(entity);
		}

		public void Delete(T entity)
		{
			ThrowIfDisposed();
			DataManipulationRepository.Delete(entity);
		}

		public void Update(T oldValues, T newValues)
		{
			ThrowIfDisposed();
			DataManipulationRepository.Update(oldValues, newValues);
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
