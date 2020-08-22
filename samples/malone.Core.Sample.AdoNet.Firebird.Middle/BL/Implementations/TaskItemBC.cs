using malone.Core.Business.Components;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.Sample.AdoNet.Firebird.Middle.CL.Exceptions;
using malone.Core.Sample.AdoNet.Firebird.Middle.EL.Model;

namespace malone.Core.Sample.AdoNet.Firebird.Middle.BL.Implementations
{
    public class TaskItemBC : BusinessComponent<TaskItem, IBusinessValidator<TaskItem>>, ITaskItemBC
    {
        public TaskItemBC(IBusinessValidator<TaskItem> businessValidator, IRepository<TaskItem> repository, ILogger logger)
            : base(businessValidator, repository, logger)
        {

        }
    }
}
