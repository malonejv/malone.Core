using ErgaOmnes.Core.CL.Exceptions;
using ErgaOmnes.Core.EL.Model;
using malone.Core.BL.Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgaOmnes.Core.BL
{
    public interface IEjemploBV : IBusinessValidator<Ejemplo, ErrorCodes>
    {
        ValidationResult ValidarCaracteresEspeciales(params object[] args);

    }
}
