//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:15</date>

using System;
using malone.Core.DataAccess.Context;
using malone.Core.IoC;

namespace malone.Core.DataAccess.UnitOfWork
	{
	public class UnitOfWork : IUnitOfWork
    {
        public IContext Context { get; protected set; }

        public UnitOfWork(IContext context)
        {
            Context = context;
        }

        public static IUnitOfWork Create()
        {
            var context = ServiceLocator.Current.Get<IContext>();
            return new UnitOfWork(context);
        }

        public int SaveChanges()
        {
            this.ThrowIfDisposed();
            return Context.SaveChanges();
        }

        private bool _disposed = false;

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
    }
}
