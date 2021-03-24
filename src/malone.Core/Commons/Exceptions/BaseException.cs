//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:51</date>

using System;

namespace malone.Core.Commons.Exceptions
{
    /// <summary>
    /// Defines the <see cref="BaseException" />.
    /// </summary>
    public abstract class BaseException : Exception
    {
        /// <summary>
        /// Defines the SUPPORT_ID.
        /// </summary>
        public const string SUPPORT_ID = "SupportId";

        /// <summary>
        /// Defines the ERROR_CODE.
        /// </summary>
        public const string ERROR_CODE = "ErrorCode";

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        public BaseException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public BaseException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
