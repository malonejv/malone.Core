using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Core.EL.Model
{
    public enum RoleType
    {
        [Description("Administrador")]
        Administrador,
        [Description("Administrativo")]
        Administrativo,
        [Description("Empleado")]
        Empleado,
        [Description("Usuario")]
        Usuario
    }
}
