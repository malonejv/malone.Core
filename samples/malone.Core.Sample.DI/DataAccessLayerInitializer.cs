using malone.Core.Commons.Initializers;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.EF.Repositories.Implementations;
using malone.Core.Sample.Middle.DAL.Context.EF;
using malone.Core.Sample.Middle.EL.Model;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace malone.Core.Sample.DI
{
    public class DataAccessLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            #region EF

            //Context
            container.RegisterType<IContext, SampleEFContext>(new PerRequestLifetimeManager(), new InjectionConstructor("SampleConnection"));
            //            container.RegisterType<EFIdentityDbContext, SampleEFContext>(new PerRequestLifetimeManager(), new InjectionConstructor("SampleConnection"));
            //            container.RegisterType<IDatabaseInitializer<SampleEFContext>, SampleContextInitializer>();

            //            IDatabaseInitializer<SampleEFContext> initializer = null;
            //            //TODO: Habilitar configuración para SampleContextInitializer
            //#if DEBUG
            //            initializer = container.Resolve<IDatabaseInitializer<SampleEFContext>>();
            //#endif
            //            //OPTION: Habilita la clase SampleContextInitializer 
            //            Database.SetInitializer<SampleEFContext>(initializer);

            //Repositories
            container.RegisterType<IRepository<TodoList>, EFRepository<TodoList>>();
            container.RegisterType<IRepository<TaskItem>, EFRepository<TaskItem>>();

            //container.RegisterType<IRoleStore<CoreRole, int>, RoleRepository<EFIdentityDbContext>>();
            //container.RegisterType<IUserStore<CoreUser, int>, UserRepository<EFIdentityDbContext>>();

            #endregion

            //General
            //container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            //container.RegisterType<IPasswordHasher, PasswordHasher>();

        }
    }
}
