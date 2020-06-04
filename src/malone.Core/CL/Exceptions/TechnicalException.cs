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
        public TechnicalException(TCode code)
            : base(code) { }

        public TechnicalException(TCode code, bool rethrow = false, bool shouldLog = true)
            : base(code, rethrow, shouldLog) { }

        public TechnicalException(TCode code, string message, bool rethrow = false, bool shouldLog = true)
            : base(code, message, rethrow, shouldLog) { }

        public TechnicalException(TCode code, string message, Exception innerException, bool rethrow = false, bool shouldLog = true)
            : base(code, message, innerException, rethrow, shouldLog) { }
    }
}
