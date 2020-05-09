using malone.Core.CL.DI;
using malone.Core.Identity.EntityFramework.EL;
using malone.Core.Sample.Middle.EL.Model;
using Unity;

namespace malone.Core.Sample.DI
{
    public class EntitiesLayerBootstrapper : ILayerBootstrapper<IUnityContainer>
    {
        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<CoreUser>();
            container.RegisterType<CoreRole>();
            container.RegisterType<CoreUserLogin>();
            container.RegisterType<CoreUserRole>();
            container.RegisterType<CoreUserClaim>();
            container.RegisterType<TodoList>();

            container.RegisterType<TodoList>();
            container.RegisterType<TaskItem>();

        }
    }
}
