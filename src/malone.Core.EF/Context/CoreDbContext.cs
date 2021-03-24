using malone.Core.DataAccess.Context;
using System.Data.Entity;

namespace malone.Core.EF.Context
{
    //TODO: Analizar - Debería ser abstract? Debería existir? Debería implementar IEFContext en lugar de IContext.
    public abstract class CoreDbContext : DbContext, IContext
    {
        public CoreDbContext() : base()
        { }

        public CoreDbContext(string connectionStringName) : base(connectionStringName)
        {
        }

    }
}
