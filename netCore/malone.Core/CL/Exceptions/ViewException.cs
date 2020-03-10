using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions
{
    public class ViewException : BaseException
    {
        public ViewException()
            : base()
        {
        }

        public ViewException(string message)
            : base(message)
        {
        }


        public ViewException(string message, bool rethrow)
            : base(message, rethrow)
        {
        }

        public ViewException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ViewException(string message, Exception innerException, bool rethrow)
            : base(message, innerException, rethrow)
        {
        }

        public ViewException(int code, string message)
            : base(code, message)
        {
        }

        public ViewException(int code, string message, bool rethrow)
            : base(code, message, rethrow)
        {
        }

        public ViewException(int code, string message, Exception innerException)
            : base(code, message, innerException)
        {
        }

        public ViewException(int code, string message, Exception innerException, bool rethrow)
            : base(code, message, innerException, rethrow)
        {
        }
    }
}
