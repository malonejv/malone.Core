using malone.Core.Logging;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;
using malone.Core.Services;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.BL.Implementations
{
	public class TaskItemBC : Service<TaskItem, ITaskItemBV>, ITaskItemBC
	{
		public TaskItemBC(ITaskItemBV businessValidator, IQueryService<int, TaskItem, ITaskItemBV> queryService, IDataManipulationService<int, TaskItem, ITaskItemBV> dataManipulationService, ICoreLogger logger)
			: base(businessValidator, queryService, dataManipulationService, logger)
		{ }

    }
}
