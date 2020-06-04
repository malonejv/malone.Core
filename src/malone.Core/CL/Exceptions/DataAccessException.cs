using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.CL.Exceptions;

namespace malone.Core.CL.Exceptions
{
    /// <summary>
    /// Encapsulates general exceptions in Data Access Layer
    /// </summary>
    public class DataAccessException<TCode> : BaseException<TCode>
                where TCode : Enum
    {
        public DataAccessException(TCode code)
            : base(code) { }

        public DataAccessException(TCode code, bool rethrow = false, bool shouldLog = true)
            : base(code, rethrow, shouldLog) { }

        public DataAccessException(TCode code, string message, bool rethrow = false, bool shouldLog = true)
            : base(code, message, rethrow, shouldLog) { }

        public DataAccessException(TCode code, string message, Exception innerException, bool rethrow = false, bool shouldLog = true)
            : base(code, message, innerException, rethrow, shouldLog) { }
    }
}
