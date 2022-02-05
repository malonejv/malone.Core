//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:52</date>

using System;

namespace malone.Core.Commons.Exceptions
{
    public class BusinessValidationException : BaseException
    {
        public BusinessValidationException()
: base()
        {
        }

        public BusinessValidationException(string message)
: base(message)
        {
        }

        public BusinessValidationException(string message, Exception innerException)
: base(message, innerException)
        {
        }
    }
}
