using System.Data.Entity;
using malone.Core.Commons.DI;
using malone.Core.Commons.Initializers;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.EF.Repositories.Implementations;
using malone.Core.Identity.EntityFramework.Context;
using malone.Core.IoC;
using malone.Core.Sample.EF.SqlServer.Middle.DAL.Context;
using malone.Core.Sample.EF.SqlServer.Middle.DAL.Repositories;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace malone.Core.Sample.EF.SqlServer.DI
{
    public class DataAccessLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //Context
            container.RegisterType<IContext, SampleContext>(new PerRequestLifetimeManager(), new InjectionConstructor("SampleConnection"));

            //Agrego esta configuración por la siguiente configuracion: container.RegisterType<IRoleStore<CoreRole, int>, RoleRepository<EFIdentityDbContext>>();
            var context = ServiceLocator.Current.Get<IContext>();
            container.RegisterInstance(context as DbContext);
            
            //Repositories
            container.RegisterType<IRepository<TodoList>, TodoListRepository>();
            container.RegisterType<IRepository<TaskItem>, Repository<TaskItem>>();

        }
    }
}
