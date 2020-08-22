using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Commons.DI;
using malone.Core.DataAccess.Context;

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

        #region Dispose
        
        private bool _disposed = false;

        /// <summary>
        ///     Dispose the store
        /// </summary>
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

        /// <summary>
        ///     If disposing, calls dispose on the Context.  Always nulls out the Context
        /// </summary>
        /// <param name="disposing"></param>
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
