using malone.Core.Identity.EntityFramework.EL;
using malone.Core.Sample.Middle.EL.Model;
using Unity;

namespace malone.core.Sample.DI
{

    public static class RegisterEntitesLayer
    {
        public static IUnityContainer RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<CoreUser>();
            container.RegisterType<CoreRole>();
            container.RegisterType<CoreUserLogin>();
            container.RegisterType<CoreUserRole>();
            container.RegisterType<CoreUserClaim>();
            container.RegisterType<TodoList>();

            container.RegisterType<TodoList>();
            container.RegisterType<TaskItem>();

            return container;
        }
    }
}
