using malone.Core.Business.Components;
using malone.Core.Sample.EF.Firebird.Middle.EL.Model;

namespace malone.Core.Sample.EF.Firebird.Middle.BL
{
    public interface ITaskItemBC : IBusinessComponent<TaskItem, IBusinessValidator<TaskItem>>
    {
    }
}
