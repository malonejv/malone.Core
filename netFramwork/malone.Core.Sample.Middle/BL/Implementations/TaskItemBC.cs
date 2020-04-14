using malone.Core.BL.Components.Implementations;
using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Exceptions.Handler.Interfaces;
using malone.Core.CL.Exceptions.Manager.Interfaces;
using malone.Core.DAL.Repositories;
using malone.Core.DAL.UnitOfWork;
using malone.Core.Sample.Middle.EL.Model;

namespace malone.Core.Sample.Middle.BL.Implementations
{
    public class TaskItemBC : BusinessComponent<decimal, TaskItem, IBusinessValidator<decimal, TaskItem>>, ITaskItemBC
    {
        public TaskItemBC(IUnitOfWork unitOfWork, IBusinessValidator<decimal, TaskItem> businessValidator, IRepository<decimal, TaskItem> repository, IExceptionMessageManager exManager, IExceptionHandler exHandler)
            : base(unitOfWork, businessValidator, repository, exManager, exHandler)
        {

        }
    }
}
