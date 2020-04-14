using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions
{
    public enum CoreErrors
    {
        #region GeneralErrors 0 - 99
        E0 = 0,
        #endregion

        #region Presentation Errors 100 - 199
        E100 = 100,
        #endregion

        #region ServiceErrors 200 - 299
        E200 = 200,
        #endregion

        #region BusinessErrors 300 - 399
        /// <summary>
        /// Se produjo un error al intentar obtener los datos solicitados.
        /// </summary>
        E300 = 300,
        /// <summary>
        /// Se produjo un error al intentar guardar los datos solicitados.
        /// </summary>
        E301 = 301,
        /// <summary>
        /// Se produjo un error al intentar eliminar el dato solicitado.
        /// </summary>
        E302 = 302,
        /// <summary>
        /// Se produjo un error al intentar actualizar los datos solicitados.
        /// </summary>
        E303 = 303,
        /// <summary>
        /// No se encontraron resultados para la consulta realizada.
        /// </summary>
        E304 = 304,
        /// <summary>
        /// Nombre de usuario o contraseña invalido.
        /// </summary>
        E305 = 305,
        /// <summary>
        /// Debes confirmar el email primero.
        /// </summary>
        E306 = 306,
        /// <summary>
        /// Usuario no autorizado.
        /// </summary>
        E307 = 307,
        #endregion

        #region DataAccessErrors 400 - 479
        /// <summary>
        /// Error al obtener una lista ordenada de tipo {0}.
        /// </summary>
        E400 = 400, 
        /// <summary>
        /// Error al obtener una entidad de tipo {0}.
        /// </summary>
        E401 = 401, 
        /// <summary>
        /// Error al insertar una entidad de tipo {0}.
        /// </summary>
        E402 = 402, 
        /// <summary>
        /// Error al eliminar una entidad de tipo {0}.
        /// </summary>
        E403 = 403,
        /// <summary>
        /// Error al actualizar una entidad de tipo {0}.
        /// </summary>
        E404 = 404,
        #endregion

        #region ServiceAgentErrors 480 - 499
        /// <summary>
        /// Se produjo un error en la llamada al servicio {0}.
        /// </summary>
        E480 = 480, 
        #endregion

        #region EntitiesErrors 500 - 599
        E500 = 500,
        #endregion

        #region CommonErrors 600 - 699
        E600 = 600
        #endregion
    }
}
