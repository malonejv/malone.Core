using Unity;
using malone.Core.Sample.Middle.EL;
using malone.Core.Sample.Middle.EL.Model;

namespace malone.core.Sample.DI
{

    public static class RegisterEntitesLayer
    {
        public static IUnityContainer RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<TodoList>();
            container.RegisterType<TaskItem>();

            return container;
        }
    }
}
