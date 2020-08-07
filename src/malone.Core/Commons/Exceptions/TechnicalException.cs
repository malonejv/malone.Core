using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Exceptions
{
    /// <summary>
    /// Encapsulates general exceptions of Technical purpose
    /// </summary>
    public class TechnicalException : BaseException
    {
        public TechnicalException()
            : base() { }

        public TechnicalException(string message)
            : base(message) { }

        public TechnicalException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
