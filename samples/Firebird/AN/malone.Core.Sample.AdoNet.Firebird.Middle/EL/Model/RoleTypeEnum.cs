﻿using System.ComponentModel;

namespace malone.Core.Sample.AdoNet.Firebird.Middle.EL.Model
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