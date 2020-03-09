using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.DAL.Base.Context;

namespace malone.Core.DAL.Base.UnitOfWork
{
    public interface IUnitOfWork
    {
        IContext Context { get; }

        int SaveChanges();

        void Dispose();
    }
}
