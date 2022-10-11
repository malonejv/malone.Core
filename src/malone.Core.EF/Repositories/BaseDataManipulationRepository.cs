using System;
using System.Data.Entity;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.Logging;

namespace malone.Core.EF.Repositories
{
	public class BaseCUDRepository<T> : IBaseCUDRepository<T>, IDisposable
		where T : class
	{
		protected DbSet<T> EntityDbSet { get; private set; }
		protected DbContext Context { get; private set; }
		protected ICoreLogger Logger { get; set; }

		#region Constructor

		public BaseCUDRepository(IContext context, ICoreLogger logger)
		{
			Context = context.ThrowIfNull().ThrowIfNotOfType<IContext, DbContext>();
			Logger = logger.ThrowIfNull();
			EntityDbSet = Context.Set<T>();
		}

		#endregion

		#region Public methods

		public virtual void Insert(T entity)
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

		public virtual void Update(T oldValues, T newValues)
		{
			ThrowIfDisposed();
			try
			{
				Context.Entry(oldValues).State = EntityState.Detached;
				Context.Entry(newValues).State = EntityState.Modified;
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
				if (Context.Entry(entity).State == EntityState.Detached)
				{
					EntityDbSet.Attach(entity);
				}
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
