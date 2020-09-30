using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Sample.EF.SqlServer.Middle.CL.Exceptions
{
    public enum ErrorCode
    {

        #region Technical Errors 1000 - 2999

        /// <summary>
        /// Technical error description
        /// </summary>
        TECH1000 = 1,

        #endregion

        #region Services Errors 3000 - 3999

        /// <summary>
        /// Service error description
        /// </summary>
        SERVICE3000 = 3000,

        #endregion

        #region Business Errors 4000 - 4999
        /// <summary>
        /// Business error description
        /// </summary>
        BUSINESS4000 = 4000,

        #endregion

        #region Business Validations Errors 5000 - 5999

        /// <summary>
        /// ValidarCaracteresEspeciales
        /// </summary>
        BUSVAL5000 = 5000,

        /// <summary>
        /// ValidarNombreRepetido
        /// </summary>
        BUSVAL5001 = 5001, 

        #endregion

        #region Data Access Errors 6000 - 6999

        /// <summary>
        /// Data access error description
        /// </summary>
        DATAACCESS6000 = 6000,

        #endregion

        #region Service Agent Errors 7000 - 7999

        /// <summary>
        /// Se produjo un error en la llamada al servicio {0}.
        /// </summary>
        SERVAG7000 = 7000

        #endregion


    }
}
