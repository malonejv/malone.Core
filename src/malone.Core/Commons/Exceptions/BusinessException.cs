using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Exceptions
{
    /// <summary>
    /// Encapsulates general exceptions in Business Layer
    /// </summary>
    public class BusinessException<TCode> : BaseException<TCode>
        where TCode : Enum
    {
        public BusinessException(TCode code)
            : base(code) { }

        public BusinessException(TCode code, bool rethrow = false, bool shouldLog = true)
            : base(code, rethrow, shouldLog) { }

        public BusinessException(TCode code, string message, bool rethrow = true, bool shouldLog = true)
            : base(code, message, rethrow, shouldLog) { }

        public BusinessException(TCode code, string message, Exception innerException, bool rethrow = true, bool shouldLog = true)
            : base(code, message, innerException, rethrow, shouldLog) { }
    }
}
