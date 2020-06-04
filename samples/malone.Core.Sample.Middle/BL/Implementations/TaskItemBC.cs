using malone.Core.BL.Components.Implementations;
using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.DAL.Repositories;
using malone.Core.DAL.UnitOfWork;
using malone.Core.Sample.Middle.CL.Exceptions;
using malone.Core.Sample.Middle.EL.Model;

namespace malone.Core.Sample.Middle.BL.Implementations
{
    public class TaskItemBC : BusinessComponent<TaskItem, IBusinessValidator<TaskItem, ErrorCodes>, ErrorCodes>, ITaskItemBC
    {
        public TaskItemBC(IUnitOfWork unitOfWork, IBusinessValidator<TaskItem, ErrorCodes> businessValidator, ICoreRepository<TaskItem, ErrorCodes> repository, IExceptionHandler<ErrorCodes> exceptionHandler)
            : base(unitOfWork, businessValidator, repository, exceptionHandler)
        {

        }
    }
}
