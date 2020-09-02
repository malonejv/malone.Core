using malone.Core.Business.Components;
using malone.Core.Sample.EF.Firebird.Middle.EL.Model;

namespace malone.Core.Sample.EF.Firebird.Middle.BL
{
    public interface ITodoListBV : IBusinessValidator<TodoList>
    {
        ValidationResult ValidarNombreRepetido(params object[] args);
    }
}
