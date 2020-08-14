using GMS.Core.EL.Model;
using malone.Core.Commons.Initializers;
using Unity;

namespace GMS.Core.Initializers
{
    public class EntitiesLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<TodoList>();
            container.RegisterType<TaskItem>();

        }
    }
}
