using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
