using malone.Core.AdoNet.Repositories;
using malone.Core.Commons.Initializers;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.Sample.AdoNet.Firebird.Middle.DAL.Context;
using malone.Core.Sample.AdoNet.Firebird.Middle.DAL.Repositories;
using malone.Core.Sample.AdoNet.Firebird.Middle.EL.Model;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace malone.Core.Sample.AdoNet.Firebird.DI
{
    public class DataAccessLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //Context
            container.RegisterType<IContext, SampleAdoNetContext>(new PerRequestLifetimeManager(), new InjectionConstructor("SampleConnection"));
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
            container.RegisterType<IRepository<TodoList>, TodoListRepository>();
            container.RegisterType<IRepository<TaskItem>, AdoNetRepository<TaskItem>>();

            //container.RegisterType<IRoleStore<CoreRole, int>, RoleRepository<EFIdentityDbContext>>();
            //container.RegisterType<IUserStore<CoreUser, int>, UserRepository<EFIdentityDbContext>>();

            //General
            //container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            //container.RegisterType<IPasswordHasher, PasswordHasher>();

        }
    }
}
