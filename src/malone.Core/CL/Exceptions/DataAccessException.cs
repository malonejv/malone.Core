using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.CL.Exceptions;

namespace malone.Core.CL.Exceptions
{
    public class DataAccessException : BaseException
    {
        public DataAccessException()
            : base()
        {
            Rethrow = false;
        }

        public DataAccessException(string message)
            : base(message)
        {
            Rethrow = false;
        }


        public DataAccessException(string message, bool rethrow)
            : base(message, rethrow)
        {
            Rethrow = false;
        }

        public DataAccessException(string message, Exception innerException)
            : base(message, innerException)
        {
            Rethrow = false;
        }

        public DataAccessException(string message, Exception innerException, bool rethrow)
            : base(message, innerException, rethrow)
        {
            Rethrow = false;
        }

        public DataAccessException(int code, string message)
            : base(code, message)
        {
            Rethrow = false;
        }

        public DataAccessException(int code, string message, bool rethrow)
            : base(code, message, rethrow)
        {
            Rethrow = false;
        }

        public DataAccessException(int code, string message, Exception innerException)
            : base(code, message, innerException)
        {
            Rethrow = false;
        }

        public DataAccessException(int code, string message, Exception innerException, bool rethrow)
            : base(code, message, innerException, rethrow)
        {
            Rethrow = false;
        }

    }
}
