using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Exceptions
{
    public enum CoreErrors
    {
        #region General Errors 0 - 299
        E0 = 0,
        #endregion

        #region Services Errors 300 - 399
        E300 = 300,
        #endregion

        #region Business Errors 400 - 499
        /// <summary>
        /// Se produjo un error al intentar obtener los datos solicitados.
        /// </summary>
        E400 = 400,
        /// <summary>
        /// Se produjo un error al intentar guardar los datos solicitados.
        /// </summary>
        E401 = 401,
        /// <summary>
        /// Se produjo un error al intentar eliminar el dato solicitado.
        /// </summary>
        E402 = 402,
        /// <summary>
        /// Se produjo un error al intentar actualizar los datos solicitados.
        /// </summary>
        E403 = 403,
        /// <summary>
        /// No se encontraron resultados para la consulta realizada.
        /// </summary>
        E404 = 404,
        /// <summary>
        /// Nombre de usuario o contraseña invalido.
        /// </summary>
        E405 = 405,
        /// <summary>
        /// Debes confirmar el email primero.
        /// </summary>
        E406 = 406,
        /// <summary>
        /// Usuario no autorizado.
        /// </summary>
        E407 = 407,
        #endregion

        #region Business Validations Errors 500 - 599
        #endregion

        #region Data Access Errors 600 - 699
            //EFErrors.cs
            //AdoNetErrors.cs
        #endregion

        #region Service Agent Errors 700 - 799
        /// <summary>
        /// Se produjo un error en la llamada al servicio {0}.
        /// </summary>
        E700 = 700, 
        #endregion

        #region Technical Errors 800 - 899
        
        /// <summary>
        /// Technical Error description
        /// </summary>
        E800 = 800

        #endregion
    }
}
