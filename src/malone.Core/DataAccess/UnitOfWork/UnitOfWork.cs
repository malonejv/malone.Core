//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:15</date>

using malone.Core.Commons.DI;
using malone.Core.DataAccess.Context;
using System;

namespace malone.Core.DataAccess.UnitOfWork
{
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
        /// Dispose the store.
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
        /// If disposing, calls dispose on the Context.  Always nulls out the Context.
        /// </summary>
        /// <param name="disposing">.</param>
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
