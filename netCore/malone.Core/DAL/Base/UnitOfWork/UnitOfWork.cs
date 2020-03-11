﻿using malone.Core.DAL.Base.Context;
using System;

namespace malone.Core.DAL.Base.UnitOfWork
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
