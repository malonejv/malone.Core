using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.AdoNet.Exceptions
{
    internal enum AdoNetErrors
    {
        #region Data Access Errors 600 - 699
        /// <summary>
        /// Error al obtener una lista ordenada de tipo {0}.
        /// </summary>
        E600 = 600,
        /// <summary>
        /// Error al obtener una entidad de tipo {0}.
        /// </summary>
        E601 = 601,
        /// <summary>
        /// Error al insertar una entidad de tipo {0}.
        /// </summary>
        E602 = 602,
        /// <summary>
        /// Error al eliminar una entidad de tipo {0}.
        /// </summary>
        E603 = 603,
        /// <summary>
        /// Error al actualizar una entidad de tipo {0}.
        /// </summary>
        E604 = 604,
        #endregion
    }
}
