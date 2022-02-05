//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:51</date>

using System;

namespace malone.Core.Commons.Exceptions
{
    public abstract class BaseException : Exception
    {
        public const string SUPPORT_ID = "SupportId";

        public const string ERROR_CODE = "ErrorCode";

        public BaseException() : base()
        {
        }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
