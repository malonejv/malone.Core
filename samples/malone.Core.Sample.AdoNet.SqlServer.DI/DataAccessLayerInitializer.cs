using malone.Core.AdoNet.Repositories;
using malone.Core.Commons.Initializers;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.Sample.AdoNet.SqlServer.Middle.DAL.Context;
using malone.Core.Sample.AdoNet.SqlServer.Middle.DAL.Repositories;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace malone.Core.Sample.AdoNet.SqlServer.DI
{
    public class DataAccessLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //Context
            container.RegisterType<IContext, SampleContext>(new PerRequestLifetimeManager(), new InjectionConstructor("SampleConnection"));
            //Repositories
            container.RegisterType<IRepository<TodoList>, TodoListRepository>();
            container.RegisterType<IRepository<TaskItem>, TaskItemRepository>();

        }
    }
}
