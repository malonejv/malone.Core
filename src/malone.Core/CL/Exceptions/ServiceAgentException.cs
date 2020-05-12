using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions
{
    public class ServiceAgentException : DataAccessException
    {
        public ServiceAgentException()
            : base()
        {
            Rethrow = false;
        }

        public ServiceAgentException(string message)
            : base(message)
        {
            Rethrow = false;
        }


        public ServiceAgentException(string message, bool rethrow)
            : base(message, rethrow)
        {
            Rethrow = false;
        }

        public ServiceAgentException(string message, Exception innerException)
            : base(message, innerException)
        {
            Rethrow = false;
        }

        public ServiceAgentException(string message, Exception innerException, bool rethrow)
            : base(message, innerException, rethrow)
        {
            Rethrow = false;
        }

        public ServiceAgentException(int code, string message)
            : base(code, message)
        {
            Rethrow = false;
        }

        public ServiceAgentException(int code, string message, bool rethrow)
            : base(code, message, rethrow)
        {
            Rethrow = false;
        }

        public ServiceAgentException(int code, string message, Exception innerException)
            : base(code, message, innerException)
        {
            Rethrow = false;
        }

        public ServiceAgentException(int code, string message, Exception innerException, bool rethrow)
            : base(code, message, innerException, rethrow)
        {
            Rethrow = false;
        }

    }
}
