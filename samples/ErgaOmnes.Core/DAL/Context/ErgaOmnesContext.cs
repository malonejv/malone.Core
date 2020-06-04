using malone.Core.Identity.EntityFramework.DAL.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ErgaOmnes.Core.DAL.Mappings;

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
