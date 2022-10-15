using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;
using malone.Core.Services;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.BL
{
	public interface ITodoListBV : IServiceValidator<TodoList>
    {
        ValidationResult ValidarCaracteresEspeciales(params object[] args);
        ValidationResult ValidarNombreRepetido(params object[] args);
    }
}
