using malone.Core.Services;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;

namespace malone.Core.Sample.EF.SqlServer.Middle.BL
{
    public interface ITodoListBV : IServiceValidator<TodoList>
    {
        ValidationResult ValidarCaracteresEspeciales(params object[] args);
        ValidationResult ValidarNombreRepetido(params object[] args);
    }
}
