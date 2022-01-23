using malone.Core.Business.Components;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using GMS.Core.CL.Exceptions;
using GMS.Core.EL.Model;

namespace GMS.Core.BL.Implementations
{
    public class TaskItemBC : BusinessComponent<TaskItem, IBusinessValidator<TaskItem>>, ITaskItemBC
    {
        public TaskItemBC(IUnitOfWork unitOfWork, IBusinessValidator<TaskItem> businessValidator, IRepository<TaskItem> repository, ILogger logger)
            : base(unitOfWork, businessValidator, repository, logger)
        {

        }
    }
}
