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

        public ServiceAgentException()
            : base()
        {
            Rethrow = false;
        }

        public ServiceAgentException(TCode code, string message)
            : base(code, message)
        {
            Rethrow = false;
        }

        public ServiceAgentException(TCode code, string message, bool rethrow)
            : base(code, message, rethrow)
        {
            Rethrow = false;
        }

        public ServiceAgentException(TCode code, string message, Exception innerException)
            : base(code, message, innerException)
        {
            Rethrow = false;
        }

        public ServiceAgentException(TCode code, string message, Exception innerException, bool rethrow)
            : base(code, message, innerException, rethrow)
        {
            Rethrow = false;
        }

    }


    public class ServiceAgentException : DataAccessException<CoreErrors>
    {

        public ServiceAgentException()
            : base()
        {
            Rethrow = false;
        }

        public ServiceAgentException(CoreErrors code, string message)
            : base(code, message)
        {
            Rethrow = false;
        }

        public ServiceAgentException(CoreErrors code, string message, bool rethrow)
            : base(code, message, rethrow)
        {
            Rethrow = false;
        }

        public ServiceAgentException(CoreErrors code, string message, Exception innerException)
            : base(code, message, innerException)
        {
            Rethrow = false;
        }

        public ServiceAgentException(CoreErrors code, string message, Exception innerException, bool rethrow)
            : base(code, message, innerException, rethrow)
        {
            Rethrow = false;
        }

    }
}
