using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.DataAccess.Context;

namespace malone.Core.EF.Firebird.Context
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
