using malone.Core.CL.Initializers;
using malone.Core.DAL.Context;
using malone.Core.DAL.EF.Repositories.Identity;
using malone.Core.DAL.Repositories;
using malone.Core.DAL.UnitOfWork;
using malone.Core.EF.DAL.Repositories.Implementations;
using malone.Core.Identity.EntityFramework.DAL.EF.Context;
using malone.Core.Identity.EntityFramework.EL;
using malone.Core.Sample.Middle.CL.Exceptions;
using malone.Core.Sample.Middle.DAL.Context.EF;
using malone.Core.Sample.Middle.EL.Model;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace malone.Core.Sample.DI
{
    public  class DataAccessLayerInitializer : ILayer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
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
            container.RegisterType<ICoreRepository<TodoList,ErrorCodes>, EFRepository<TodoList, ErrorCodes>>();
            container.RegisterType<ICoreRepository<TaskItem, ErrorCodes>, EFRepository<TaskItem, ErrorCodes>>();

            container.RegisterType<IRoleStore<CoreRole, int>, RoleRepository<EFIdentityDbContext>>();
            container.RegisterType<IUserStore<CoreUser, int>, UserRepository<EFIdentityDbContext>>();

            #endregion

            //General
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            //container.RegisterType<IPasswordHasher, PasswordHasher>();

        }
    }
}
