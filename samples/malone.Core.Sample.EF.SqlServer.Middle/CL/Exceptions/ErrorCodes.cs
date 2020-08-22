using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Sample.EF.SqlServer.Middle.CL.Exceptions
{
    public enum ErrorCodes
    {
        #region GeneralErrors 1000 - 1099
        E1000 = 1000,
        #endregion

        #region Presentation Errors 1100 - 1199

        #endregion

        #region ServiceErrors 1200 - 1299

        #endregion

        #region BusinessErrors 1300 - 1399
        /// <summary>
        /// Nombre Repetido
        /// </summary>
        E1300 = 1300
        #endregion

        #region DataAccessErrors 1400 - 1479

        #endregion
    }
}
