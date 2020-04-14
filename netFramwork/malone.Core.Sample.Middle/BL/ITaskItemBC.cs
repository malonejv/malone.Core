using malone.Core.BL.Components.Interfaces;
using malone.Core.Sample.Middle.EL.Model;

namespace malone.Core.Sample.Middle.BL
{
    public interface ITaskItemBC : IBusinessComponent<decimal, TaskItem, IBusinessValidator<decimal, TaskItem>>
    {
    }
}
