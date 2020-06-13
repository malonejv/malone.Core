using malone.Core.Business.Components;
using malone.Core.Sample.Middle.EL.Model;

namespace malone.Core.Sample.Middle.BL
{
    public interface ITaskItemBC : IBusinessComponent<TaskItem, IBusinessValidator<TaskItem>>
    {
    }
}
