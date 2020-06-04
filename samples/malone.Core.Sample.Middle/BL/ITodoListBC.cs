using malone.Core.BL.Components.Interfaces;
using malone.Core.Sample.Middle.CL.Exceptions;
using malone.Core.Sample.Middle.EL.Model;

namespace malone.Core.Sample.Middle.BL
{
    public interface ITodoListBC : IBusinessComponent<TodoList, ITodoListBV, ErrorCodes>
    {
    }
}
