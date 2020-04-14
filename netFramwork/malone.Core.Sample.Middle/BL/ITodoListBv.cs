using malone.Core.BL.Components.Interfaces;
using malone.Core.Sample.Middle.EL.Model;

namespace malone.Core.Sample.Middle.BL
{
    public interface ITodoListBV : IBusinessValidator<decimal,TodoList>
    {
        ValidationFailure ValidarNombreRepetido(params object[] args);
    }
}
