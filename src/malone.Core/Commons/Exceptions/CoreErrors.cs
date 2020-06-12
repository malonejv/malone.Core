using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    internal enum CoreErrors
    {
        #region Technical Errors 1 - 299

        /// <summary>
        /// Perdón, algo salió mal.
        /// </summary>
        TECH1 = 1,

        /// <summary>
        /// Se produjo un error inesperado.
        /// </summary>
        TECH2 = 2,

        #endregion

        #region Services Errors 300 - 399

        /// <summary>
        /// Usuario no autorizado.
        /// </summary>
        SERVICE300 = 300,

        #endregion

        #region Business Errors 400 - 499

        /// <summary>
        /// Se produjo un error al intentar obtener los datos solicitados.
        /// </summary>
        BUSINESS400 = 400,
        /// <summary>
        /// Se produjo un error al intentar guardar los datos solicitados.
        /// </summary>
        BUSINESS401 = 401,
        /// <summary>
        /// Se produjo un error al intentar eliminar el dato solicitado.
        /// </summary>
        BUSINESS402 = 402,
        /// <summary>
        /// Se produjo un error al intentar actualizar los datos solicitados.
        /// </summary>
        BUSINESS403 = 403,
        /// <summary>
        /// No se encontraron resultados para la consulta realizada.
        /// </summary>
        BUSINESS404 = 404,
        /// <summary>
        /// Nombre de usuario o contraseña invalido.
        /// </summary>
        BUSINESS405 = 405,
        /// <summary>
        /// Debes confirmar el email primero.
        /// </summary>
        BUSINESS406 = 406,

        #endregion

        #region Business Validations Errors 500 - 599

        #endregion

        #region Data Access Errors 600 - 699

        /// <summary>
        /// Error al obtener una lista ordenada de tipo {0}.
        /// </summary>
        DATAACCESS600 = 600,
        /// <summary>
        /// Error al obtener una entidad de tipo {0}.
        /// </summary>
        DATAACCESS601 = 601,
        /// <summary>
        /// Error al insertar una entidad de tipo {0}.
        /// </summary>
        DATAACCESS602 = 602,
        /// <summary>
        /// Error al eliminar una entidad de tipo {0}.
        /// </summary>
        DATAACCESS603 = 603,
        /// <summary>
        /// Error al actualizar una entidad de tipo {0}.
        /// </summary>
        DATAACCESS604 = 604,

        #endregion

        #region Service Agent Errors 700 - 799

        /// <summary>
        /// Se produjo un error en la llamada al servicio {0}.
        /// </summary>
        SERVAG700 = 700

        #endregion

    }
}
