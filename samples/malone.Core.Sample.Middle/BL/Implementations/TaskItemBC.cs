using malone.Core.BL.Components.Implementations;
using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.DAL.Repositories;
using malone.Core.DAL.UnitOfWork;
using malone.Core.Sample.Middle.EL.Model;

namespace malone.Core.Sample.Middle.BL.Implementations
{
    public class TaskItemBC : BusinessComponent<TaskItem, IBusinessValidator<TaskItem>>, ITaskItemBC
    {
        public TaskItemBC(IUnitOfWork unitOfWork, IBusinessValidator<TaskItem> businessValidator, IRepository<TaskItem> repository, IExceptionHandler<CoreErrors> exceptionHandler)
            : base(unitOfWork, businessValidator, repository, exceptionHandler)
        {

        }
    }
}
