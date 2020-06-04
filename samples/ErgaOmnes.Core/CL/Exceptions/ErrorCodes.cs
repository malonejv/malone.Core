using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgaOmnes.Core.CL.Exceptions
{
    public enum ErrorCodes
    {
        #region General Errors 1000 - 1999
        /// <summary>
        /// General Error description
        /// </summary>
        E1000 = 1000,
        #endregion

        #region Presentation Errors 2000 - 2999
        /// <summary>
        /// Presentation Error description
        /// </summary>
        E2000 = 2000,

        #endregion

        #region Services Errors 3000 - 3999
        /// <summary>
        /// Service Error description
        /// </summary>
        E3000 = 3000,
        #endregion

        #region Business Errors 4000 - 4999
        /// <summary>
        /// Business Error description
        /// </summary>
        E4000 = 4000,
        #endregion

        #region Business Validations Errors 5000 - 5999
        /// <summary>
        /// ValidarCaracteresEspeciales
        /// </summary>
        E5000 = 5000,
        #endregion

        #region Data Access Errors 6000 - 6999
        /// <summary>
        /// Data Access Error description
        /// </summary>
        E6000 = 6000,
        #endregion

        #region Service Agents Errors 7000 - 7999
        /// <summary>
        /// Service Agent Error description
        /// </summary>
        E7000 = 7000,
        #endregion

        #region Technical Errors 8000 - 8999
        /// <summary>
        /// Argumento no válido
        /// </summary>
        E8000 = 8000
        #endregion

    }
}
