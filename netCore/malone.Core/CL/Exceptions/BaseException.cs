using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions
{
    public abstract class BaseException : Exception
    {
        [DefaultValue(true)]
        public bool Rethrow { get; protected set; }

        [DefaultValue(true)]
        public bool ShouldLog { get; protected set; }

        [DefaultValue(null)]
        public int? Error { get; private set; }

        public BaseException()
            : base()
        {
        }

        public BaseException(string message)
            : base(message)
        {
        }


        public BaseException(string message, bool rethrow)
            : base(message)
        {
            Rethrow = rethrow;
        }

        public BaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BaseException(string message, Exception innerException, bool rethrow)
            : base(message, innerException)
        {
            Rethrow = rethrow;
        }

        public BaseException(int code, string message)
            : base(message)
        {
            Error = code;
        }

        public BaseException(int code, string message, bool rethrow)
            : this(message, rethrow)
        {
            Error = code;
        }

        public BaseException(int code, string message, Exception innerException)
            : this(message, innerException)
        {
            Error = code;
        }

        public BaseException(int code, string message, Exception innerException, bool rethrow)
            : this(message, innerException, rethrow)
        {
            Error = code;
        }
        
    }
}
