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
        public DataAccessException()
            : base()
        {
            Rethrow = false;
        }

        public DataAccessException(TCode code, string message)
            : base(code, message)
        {
            Rethrow = false;
        }

        public DataAccessException(TCode code, string message, bool rethrow)
            : base(code, message, rethrow)
        {
            Rethrow = false;
        }

        public DataAccessException(TCode code, string message, Exception innerException)
            : base(code, message, innerException)
        {
            Rethrow = false;
        }

        public DataAccessException(TCode code, string message, Exception innerException, bool rethrow)
            : base(code, message, innerException, rethrow)
        {
            Rethrow = false;
        }

    }

    public class DataAccessException: DataAccessException<CoreErrors>
    {

        public DataAccessException()
            : base()
        {
        }

        public DataAccessException(CoreErrors code, string message)
            : base(code, message)
        {
        }

        public DataAccessException(CoreErrors code, string message, bool rethrow)
            : base(code, message, rethrow)
        {
        }

        public DataAccessException(CoreErrors code, string message, Exception innerException)
            : base(code, message, innerException)
        {
        }

        public DataAccessException(CoreErrors code, string message, Exception innerException, bool rethrow)
            : base(code, message, innerException, rethrow)
        {
        }
    }
}
