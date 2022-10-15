using malone.Core.Commons.Initializers;
using malone.Core.Sample.AdoNet.SqlServer.Middle.BL;
using malone.Core.Sample.AdoNet.SqlServer.Middle.BL.Implementations;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;
using malone.Core.Services;
using Unity;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.Initializers
{
	public class BusinessLayerInitializer : IInitializer<IUnityContainer>
	{
		public void Initialize(IUnityContainer container)
		{
			//BUSINESS VALIDATORS
			container.RegisterType<ITodoListBV, TodoListBV>();
			container.RegisterType<IServiceValidator<TaskItem>, ServiceValidator<TaskItem>>();

			//BUSINESS COMPONENTS
			container.RegisterType<IQueryService<int, TodoList, ITodoListBV>, QueryService<int, TodoList, ITodoListBV>>();
			container.RegisterType<IDataManipulationService<int, TodoList, ITodoListBV>, DataManipulationService<int, TodoList, ITodoListBV>>();
			container.RegisterType<IQueryService<int, TaskItem, IServiceValidator<TaskItem>>, QueryService<int, TaskItem, IServiceValidator<TaskItem>>>();
			container.RegisterType<IDataManipulationService<int, TaskItem, IServiceValidator<TaskItem>>, DataManipulationService<int, TaskItem, IServiceValidator<TaskItem>>>();
			container.RegisterType<ITodoListBC, TodoListBC>();
			container.RegisterType<ITaskItemBC, TaskItemBC>();
		}
	}
}
