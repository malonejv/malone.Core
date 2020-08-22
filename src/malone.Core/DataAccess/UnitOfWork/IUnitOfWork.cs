using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.DataAccess.Context;

namespace malone.Core.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IContext Context { get; }

        int SaveChanges();

        void Dispose();
    }
}
