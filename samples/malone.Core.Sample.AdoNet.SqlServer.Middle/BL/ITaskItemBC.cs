using malone.Core.Business.Components;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.BL
{
    public interface ITaskItemBC : IBusinessComponent<TaskItem, IBusinessValidator<TaskItem>>
    {
    }
}
