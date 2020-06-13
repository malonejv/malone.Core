using ErgaOmnes.Core.DAL.Mappings;
using malone.Core.Identity.EntityFramework.Context;
using System.Data.Entity;

namespace ErgaOmnes.Core.DAL.Context
{
    public class ErgaOmnesContext : EFIdentityDbContext
    {
        public ErgaOmnesContext(string nameOrConnectionStringName) : base(nameOrConnectionStringName)
        {
            //TODO: Habilitar configuración
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer<ErgaOmnesContext>(null);
        }
        public ErgaOmnesContext() : this("ErgaOmnesConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new EjemploMapping());
        }

    }
}
