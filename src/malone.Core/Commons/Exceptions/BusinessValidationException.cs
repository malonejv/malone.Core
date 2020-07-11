using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessValidationException : BaseException
    {
        public BusinessValidationException()
            : base() { }

        public BusinessValidationException(string message)
            : base(message) { }

        public BusinessValidationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
