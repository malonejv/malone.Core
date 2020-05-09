using malone.Core.DAL.Context;
using System.Data.Entity;

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
