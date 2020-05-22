using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions
{
    /// <summary>
    /// Encapsulates general exceptions of Technical purpose
    /// </summary>
    public class TechnicalException<TCode> : BaseException<TCode>
                where TCode : Enum
    {
        public TechnicalException()
            : base()
        {
        }

        public TechnicalException(TCode code, string message)
            : base(code, message)
        {
        }

        public TechnicalException(TCode code, string message, bool rethrow)
            : base(code, message, rethrow)
        {
        }

        public TechnicalException(TCode code, string message, Exception innerException)
            : base(code, message, innerException)
        {
        }

        public TechnicalException(TCode code, string message, Exception innerException, bool rethrow)
            : base(code, message, innerException, rethrow)
        {
        }
    }


    public class TechnicalException : BaseException<CoreErrors>
    {
        public TechnicalException()
            : base()
        {
        }

        public TechnicalException(CoreErrors code, string message)
            : base(code, message)
        {
        }

        public TechnicalException(CoreErrors code, string message, bool rethrow)
            : base(code, message, rethrow)
        {
        }

        public TechnicalException(CoreErrors code, string message, Exception innerException)
            : base(code, message, innerException)
        {
        }

        public TechnicalException(CoreErrors code, string message, Exception innerException, bool rethrow)
            : base(code, message, innerException, rethrow)
        {
        }
    }
}
