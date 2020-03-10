using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.DAL.Base.Context
{
    public interface IContext
    {
        int SaveChanges();
        void Dispose();
    }
}
