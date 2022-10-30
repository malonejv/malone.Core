using malone.Core.Services;
using malone.Core.Commons.Initializers;
using malone.Core.Sample.EF.SqlServer.Middle.BL;
using malone.Core.Sample.EF.SqlServer.Middle.BL.Implementations;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using Unity;

namespace malone.Core.Sample.EF.SqlServer.Middle.Initializers
{
    public class BusinessLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //BUSINESS COMPONENTS
            container.RegisterType<ITodoListBC, TodoListBC>();
            container.RegisterType<ITaskItemBC, TaskItemBC>();
        }
    }
}
