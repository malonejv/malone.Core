﻿using System.ComponentModel;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model
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
