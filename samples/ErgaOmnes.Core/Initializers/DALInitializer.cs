using ErgaOmnes.Core.DAL.Context;
using ErgaOmnes.Core.EL.Model;
using malone.Core.Commons.Initializers;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.EF.Repositories.Implementations;
using malone.Core.Identity.EntityFramework.Context;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace ErgaOmnes.Core.Initializers
{
    public class DALInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //CONTEXT
            container.RegisterType<IContext, ErgaOmnesContext>(new SingletonLifetimeManager(), new InjectionConstructor("ErgaOmnesConnection"));
            container.RegisterType<EFIdentityDbContext, ErgaOmnesContext>(new SingletonLifetimeManager(), new InjectionConstructor("ErgaOmnesConnection"));

            //container.RegisterType<IContext, ErgaOmnesContext>(new PerRequestLifetimeManager(), new InjectionConstructor("ErgaOmnesConnection"));
            //container.RegisterType<EFIdentityDbContext, ErgaOmnesContext>(new PerRequestLifetimeManager(), new InjectionConstructor("ErgaOmnesConnection"));

            container.RegisterType<IRepository<Ejemplo>, EFRepository<Ejemplo>>();
        }
    }
}
