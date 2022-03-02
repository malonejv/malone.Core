//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:15</date>

namespace malone.Core.DataAccess.UnitOfWork
{
	using System;
	using malone.Core.DataAccess.Context;
	using malone.Core.IoC;

	/// <summary>
	/// Defines the <see cref="UnitOfWork" />.
	/// </summary>
	public class UnitOfWork : IUnitOfWork
	{
		/// <summary>
		/// Gets or sets the Context.
		/// </summary>
		public IContext Context { get; protected set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnitOfWork"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="IContext"/>.</param>
		public UnitOfWork(IContext context)
		{
			Context = context;
		}

		/// <summary>
		/// The Create.
		/// </summary>
		/// <returns>The <see cref="IUnitOfWork"/>.</returns>
		public static IUnitOfWork Create()
		{
			var context = ServiceLocator.Current.Get<IContext>();
			return new UnitOfWork(context);
		}

		/// <summary>
		/// The SaveChanges.
		/// </summary>
		/// <returns>The <see cref="int"/>.</returns>
		public int SaveChanges()
		{
			this.ThrowIfDisposed();
			return Context.SaveChanges();
		}

		/// <summary>
		/// Defines the _disposed.
		/// </summary>
		private bool _disposed = false;

		/// <summary>
		/// The Dispose.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// The ThrowIfDisposed.
		/// </summary>
		protected void ThrowIfDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException(GetType().Name);
			}
		}

		/// <summary>
		/// The Dispose.
		/// </summary>
		/// <param name="disposing">The disposing<see cref="bool"/>.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && Context != null)
			{
				Context.Dispose();
			}
			_disposed = true;
			Context = null;
		}
	}
}
