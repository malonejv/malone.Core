using malone.Core.Business.Components;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;

namespace malone.Core.Sample.EF.SqlServer.Middle.BL
{
    public interface ITaskItemBC : IBusinessComponent<TaskItem, IBusinessValidator<TaskItem>>
    {
    }
}
