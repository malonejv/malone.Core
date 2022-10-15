using malone.Core.Services;
using malone.Core.Logging;
using malone.Core.DataAccess.Repositories;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;

namespace malone.Core.Sample.EF.SqlServer.Middle.BL.Implementations
{
    public class TaskItemBC : Service<TaskItem, IServiceValidator<TaskItem>>, ITaskItemBC
    {
        public TaskItemBC(IServiceValidator<TaskItem> businessValidator, IRepository<TaskItem> repository, ICoreLogger logger)
            : base(businessValidator, repository, logger)
        {

        }
    }
}
