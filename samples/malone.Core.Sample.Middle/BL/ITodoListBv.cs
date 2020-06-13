using malone.Core.Business.Components;
using malone.Core.Sample.Middle.EL.Model;

namespace malone.Core.Sample.Middle.BL
{
    public interface ITodoListBV : IBusinessValidator<TodoList>
    {
        ValidationResult ValidarNombreRepetido(params object[] args);
    }
}
