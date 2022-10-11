using System;
using System.Data;
using malone.Core.AdoNet.Context;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.Logging;

namespace malone.Core.Dapper.Repositories
{
	public abstract class CustomRepository<TEntity> : ICustomRepository<TEntity>, IDisposable
		where TEntity : class
	{
		protected CoreDbContext Context { get; private set; }
		protected IDbConnection Connection => Context.Connection;
		protected ICoreLogger Logger { get; }

		protected Type TEntityType { get; set; }

		#region Constructor

		public CustomRepository(IContext context, ICoreLogger logger)
		{
			CheckContext(context);
			CheckLogger(logger);

			Context = (CoreDbContext)context;
			Logger = logger;
			TEntityType = this.GetType().GetInterface("ICustomRepository`1").GetGenericArguments()[0];
		}

		#endregion

		#region Private And Protected Methods

		protected abstract TEntity Map(DataRow row);

		private void CheckLogger(ICoreLogger logger)
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

			if (!(context is CoreDbContext))
			{
				//TODO: Implementar excepciones del core
				throw new ArgumentException();
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
				throw new ObjectDisposedException(this.GetType().Name);
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
