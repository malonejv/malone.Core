//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:56</date>

using System;

namespace malone.Core.Commons.Exceptions
{
    public class TechnicalException : BaseException
    {
        public TechnicalException()
: base()
        {
        }

        public TechnicalException(string message)
: base(message)
        {
        }

        public TechnicalException(string message, Exception innerException)
: base(message, innerException)
        {
        }
    }
}
