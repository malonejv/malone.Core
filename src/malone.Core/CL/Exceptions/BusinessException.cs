using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions
{
    /// <summary>
    /// Encapsulates general exceptions in Business Layer
    /// </summary>
    public class BusinessException<TCode> : BaseException<TCode>
        where TCode : Enum
    {
        public BusinessException()
            : base()
        {
        }

        public BusinessException(TCode code, string message)
            : base(code, message)
        {
        }

        public BusinessException(TCode code, string message, bool rethrow)
            : base(code, message, rethrow)
        {
        }

        public BusinessException(TCode code, string message, Exception innerException)
            : base(code, message, innerException)
        {
        }

        public BusinessException(TCode code, string message, Exception innerException, bool rethrow)
            : base(code, message, innerException, rethrow)
        {
        }
    }

    public class BusinessException : BaseException<CoreErrors>
    {
        public BusinessException()
            : base()
        {
        }

        public BusinessException(CoreErrors code, string message)
            : base(code, message)
        {
        }

        public BusinessException(CoreErrors code, string message, bool rethrow)
            : base(code, message, rethrow)
        {
        }

        public BusinessException(CoreErrors code, string message, Exception innerException)
            : base(code, message, innerException)
        {
        }

        public BusinessException(CoreErrors code, string message, Exception innerException, bool rethrow)
            : base(code, message, innerException, rethrow)
        {
        }
    }
}
