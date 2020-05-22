using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions
{
    /// <summary>
    /// Encapsulates general exceptions in View Layer
    /// </summary>
    public class ViewException<TCode> : BaseException<TCode>
    where TCode : Enum
    {

        public ViewException()
            : base()
        {
        }

        public ViewException(TCode code, string message)
            : base(code, message)
        {
        }

        public ViewException(TCode code, string message, bool rethrow)
            : base(code, message, rethrow)
        {
        }

        public ViewException(TCode code, string message, Exception innerException)
            : base(code, message, innerException)
        {
        }

        public ViewException(TCode code, string message, Exception innerException, bool rethrow)
            : base(code, message, innerException, rethrow)
        {
        }
    }


    public class ViewException : BaseException<CoreErrors>
    {

        public ViewException()
            : base()
        {
        }

        public ViewException(CoreErrors code, string message)
            : base(code, message)
        {
        }

        public ViewException(CoreErrors code, string message, bool rethrow)
            : base(code, message, rethrow)
        {
        }

        public ViewException(CoreErrors code, string message, Exception innerException)
            : base(code, message, innerException)
        {
        }

        public ViewException(CoreErrors code, string message, Exception innerException, bool rethrow)
            : base(code, message, innerException, rethrow)
        {
        }
    }
}
