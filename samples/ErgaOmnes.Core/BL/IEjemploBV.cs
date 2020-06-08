using ErgaOmnes.Core.EL.Model;
using malone.Core.Business.Components;

namespace ErgaOmnes.Core.BL
{
    public interface IEjemploBV : IBusinessValidator<Ejemplo>
    {
        ValidationResult ValidarCaracteresEspeciales(params object[] args);

    }
}
