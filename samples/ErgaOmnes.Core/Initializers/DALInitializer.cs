using ErgaOmnes.Core.CL.Exceptions;
using ErgaOmnes.Core.DAL.Context;
using ErgaOmnes.Core.DAL.Repositories;
using ErgaOmnes.Core.EL.Model;
using malone.Core.CL.Initializers;
using malone.Core.DAL.Context;
using malone.Core.DAL.EF.Repositories.Identity;
using malone.Core.DAL.UnitOfWork;
using malone.Core.Identity.EntityFramework.DAL.EF.Context;
using malone.Core.Identity.EntityFramework.EL;
using Microsoft.AspNet.Identity;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace ErgaOmnes.Core.Initializers
{
    public class DALInitializer : ILayer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //Context
            container.RegisterType<IContext, ErgaOmnesContext>(new SingletonLifetimeManager(), new InjectionConstructor("ErgaOmnesConnection"));
            container.RegisterType<EFIdentityDbContext, ErgaOmnesContext>(new SingletonLifetimeManager(), new InjectionConstructor("ErgaOmnesConnection"));

            //container.RegisterType<IContext, ErgaOmnesContext>(new PerRequestLifetimeManager(), new InjectionConstructor("ErgaOmnesConnection"));
            //container.RegisterType<EFIdentityDbContext, ErgaOmnesContext>(new PerRequestLifetimeManager(), new InjectionConstructor("ErgaOmnesConnection"));

            //Repositories
            container.RegisterType<IRepository<Ejemplo>, Repository<Ejemplo>>();

            //Identity Repositories
            container.RegisterType<IRoleStore<CoreRole, int>, RoleRepository<EFIdentityDbContext>>();
            container.RegisterType<IUserStore<CoreUser, int>, UserRepository<EFIdentityDbContext>>();


            //General
            container.RegisterType<IUnitOfWork, UnitOfWork>(new SingletonLifetimeManager());
            //container.RegisterType<IPasswordHasher, PasswordHasher>();

        }
    }
}
