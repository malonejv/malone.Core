using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions
{
    public class BusinessException : BaseException
    {
        public BusinessException()
            : base()
        {
        }

        public BusinessException(string message)
            : base(message)
        {
        }


        public BusinessException(string message, bool rethrow)
            : base(message, rethrow)
        {
        }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BusinessException(string message, Exception innerException, bool rethrow)
            : base(message, innerException, rethrow)
        {
        }

        public BusinessException(int code, string message)
            : base(code, message)
        {
        }

        public BusinessException(int code, string message, bool rethrow)
            : base(code, message, rethrow)
        {
        }

        public BusinessException(int code, string message, Exception innerException)
            : base(code, message, innerException)
        {
        }

        public BusinessException(int code, string message, Exception innerException, bool rethrow)
            : base(code, message, innerException, rethrow)
        {
        }
    }
}
