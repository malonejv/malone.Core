using malone.Core.Business.Components;
using GMS.Core.EL.Model;

namespace GMS.Core.BL
{
    public interface ITodoListBV : IBusinessValidator<TodoList>
    {
        ValidationResult ValidarNombreRepetido(params object[] args);
    }
}
