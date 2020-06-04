using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions
{
    /// <summary>
    /// Encapsulates service agent exceptions in Data Access Layer
    /// </summary>
    public class ServiceAgentException<TCode> : DataAccessException<TCode>
    where TCode : Enum
    {
        public ServiceAgentException(TCode code)
            : base(code) { }

        public ServiceAgentException(TCode code, bool rethrow = false, bool shouldLog = true)
            : base(code, rethrow, shouldLog) { }

        public ServiceAgentException(TCode code, string message, bool rethrow = false, bool shouldLog = true)
            : base(code, message, rethrow, shouldLog) { }

        public ServiceAgentException(TCode code, string message, Exception innerException, bool rethrow = false, bool shouldLog = true)
            : base(code, message, innerException, rethrow, shouldLog) { }
    }
}
