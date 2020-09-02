using malone.Core.Commons.DI;
using malone.Core.Commons.Initializers;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.EF.Repositories.Implementations;
using malone.Core.Identity.EntityFramework.Context;
using malone.Core.Sample.EF.Firebird.Middle.DAL.Context.EF;
using malone.Core.Sample.EF.Firebird.Middle.EL.Model;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace malone.Core.Sample.DI
{
    public class DataAccessLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //Context
            container.RegisterType<IContext, SampleEFContext>(new PerRequestLifetimeManager(), new InjectionConstructor("SampleConnection"));

            //Agrego esta configuración por la siguiente configuracion: container.RegisterType<IRoleStore<CoreRole, int>, RoleRepository<EFIdentityDbContext>>();
            var context = ServiceLocator.Current.Get<IContext>();
            container.RegisterInstance<EFIdentityDbContext>(context as EFIdentityDbContext);
            
            //Repositories
            container.RegisterType<IRepository<TodoList>, EFRepository<TodoList>>();
            container.RegisterType<IRepository<TaskItem>, EFRepository<TaskItem>>();

        }
    }
}
