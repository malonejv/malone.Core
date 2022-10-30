using malone.Core.Services;
using malone.Core.Logging;
using malone.Core.DataAccess.Repositories;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using malone.Core.DataAccess.UnitOfWork;

namespace malone.Core.Sample.EF.SqlServer.Middle.BL.Implementations
{
    public class TaskItemBC : Service<TaskItem>, ITaskItemBC
    {
        public TaskItemBC(IRepository<TaskItem> repository, IUnitOfWork unitOfWork, ICoreLogger logger)
            : base(logger, unitOfWork,repository)
        {

        }
    }
}
