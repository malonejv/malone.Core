using malone.Core.Business.Components;
using malone.Core.Commons.Initializers;
using malone.Core.Sample.AdoNet.Firebird.Middle.BL;
using malone.Core.Sample.AdoNet.Firebird.Middle.BL.Implementations;
using malone.Core.Sample.AdoNet.Firebird.Middle.EL.Model;
using Unity;

namespace malone.Core.Sample.AdoNet.Firebird.Middle.Initializers
{
    public class BusinessLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //BUSINESS VALIDATORS
            container.RegisterType<ITodoListBV, TodoListBV>();
            container.RegisterType<IBusinessValidator<TaskItem>, BusinessValidator<TaskItem>>();

            //BUSINESS COMPONENTS
            container.RegisterType<ITodoListBC, TodoListBC>();
            container.RegisterType<ITaskItemBC, TaskItemBC>();
        }
    }
}
