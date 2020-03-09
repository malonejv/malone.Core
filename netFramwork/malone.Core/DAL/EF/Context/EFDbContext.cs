using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.DAL.Base.Context;

namespace malone.Core.DAL.EF.Context
{
    public abstract class EFDbContext : DbContext, IContext
    {
        public EFDbContext() : base()
        { }

        public EFDbContext(string connectionStringName) : base(connectionStringName)
        {
        }
    }
}
