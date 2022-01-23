using GMS.Core.BL;
using GMS.Core.BL.Implementations;
using GMS.Core.EL.Model;
using malone.Core.Business.Components;
using malone.Core.Commons.Initializers;
using Unity;

namespace GMS.Core.Initializers
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
