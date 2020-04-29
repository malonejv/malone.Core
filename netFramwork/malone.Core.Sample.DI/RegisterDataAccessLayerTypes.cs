using malone.Core.AdoNet.DAL.Database;
using malone.Core.CL.Configurations.Sections.Feature;
using malone.Core.DAL.Context;
using malone.Core.DAL.EF.Repositories.Identity;
using malone.Core.DAL.Repositories;
using malone.Core.DAL.UnitOfWork;
using malone.Core.EF.DAL.Repositories.Implementations;
using malone.Core.Identity.EntityFramework.DAL.EF.Context;
using malone.Core.Identity.EntityFramework.EL;
using malone.Core.Sample.Middle.CL.Features;
using malone.Core.Sample.Middle.DAL.Context.AdoNet;
using malone.Core.Sample.Middle.DAL.Context.EF;
using malone.Core.Sample.Middle.DAL.Repositories.AdoNet;
using malone.Core.Sample.Middle.EL.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace malone.core.Sample.DI
{
    public static class RegisterDataAccessLayerTypes
    {
        public static IUnityContainer RegisterTypes(IUnityContainer container)
        {
            #region EF

            //Context
            container.RegisterType<IContext, SampleEFContext>(new PerRequestLifetimeManager(), new InjectionConstructor("SampleConnection"));
            container.RegisterType<EFIdentityDbContext, SampleEFContext>(new PerRequestLifetimeManager(), new InjectionConstructor("SampleConnection"));
            container.RegisterType<IDatabaseInitializer<SampleEFContext>, SampleContextInitializer>();

            IDatabaseInitializer<SampleEFContext> initializer = null;
            //TODO: Habilitar configuración para SampleContextInitializer
#if DEBUG
            initializer = container.Resolve<IDatabaseInitializer<SampleEFContext>>();
#endif
            //OPTION: Habilita la clase SampleContextInitializer 
            Database.SetInitializer<SampleEFContext>(initializer);

            //Repositories
            container.RegisterType<IRepository<TodoList>, EFRepository<TodoList>>();
            container.RegisterType<IRepository<TaskItem>, EFRepository<TaskItem>>();

            container.RegisterType<IRoleStore<CoreRole, int>, RoleRepository<EFIdentityDbContext>>();
            container.RegisterType<IUserStore<CoreUser, int>, UserRepository<EFIdentityDbContext>>();

            #endregion

            //General
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType<IPasswordHasher, PasswordHasher>();

            return container;
        }
    }
}
