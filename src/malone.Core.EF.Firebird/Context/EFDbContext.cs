using malone.Core.DataAccess.Context;
using System.Data.Entity;

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
