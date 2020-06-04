using malone.Core.CL.Initializers;
using malone.Core.Identity.EntityFramework.EL;
using malone.Core.Sample.Middle.EL.Model;
using Unity;

namespace malone.Core.Sample.DI
{
    public class EntitiesLayerInitializer : ILayer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
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
