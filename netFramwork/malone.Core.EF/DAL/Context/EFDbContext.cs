using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.DAL.Context;

namespace malone.Core.EF.DAL.Context
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
