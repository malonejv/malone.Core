using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Exceptions
{
    public abstract class BaseException : Exception
    {
        public bool Rethrow { get; protected set; }

        public bool ShouldLog { get; protected set; }

        public BaseException() : base()
        {
            Rethrow = false;
            ShouldLog = true;
        }

        public BaseException(bool rethrow = false, bool shouldLog = true) : base()
        {
            Rethrow = rethrow;
            ShouldLog = shouldLog;
        }

        public BaseException(string message, bool rethrow = false, bool shouldLog = true) : base(message)
        {
            Rethrow = rethrow;
            ShouldLog = shouldLog;
        }

        public BaseException(string message, Exception innerException, bool rethrow = false, bool shouldLog = true) : base(message, innerException)
        {
            Rethrow = rethrow;
            ShouldLog = shouldLog;
        }

    }

    public abstract class BaseException<TCode> : BaseException
        where TCode : Enum
    {
        public TCode ErrorCode { get; private set; }

        public string ErrorMessage { get { return ErrorCode.ToString() + " - " + Message; } }

        public BaseException(TCode code) : base()
        {
            if (code.Equals(default(TCode))) throw new ArgumentException(nameof(code));

            ErrorCode = code;
        }

        public BaseException(TCode code, bool rethrow = false, bool shouldLog = true) : base()
        {
            if (code.Equals(default(TCode))) throw new ArgumentException(nameof(code));

            ErrorCode = code;
            Rethrow = rethrow;
            ShouldLog = shouldLog;
        }

        public BaseException(TCode code, string message, bool rethrow = false, bool shouldLog = true)
            : base(message, rethrow, shouldLog)
        {
            if (code.Equals(default(TCode))) throw new ArgumentException(nameof(code));

            ErrorCode = code;
        }

        public BaseException(TCode code, string message, Exception innerException, bool rethrow = false, bool shouldLog = true)
            : base(message, innerException, rethrow, shouldLog)
        {
            if (code.Equals(default(TCode))) throw new ArgumentException(nameof(code));

            ErrorCode = code;
        }
    }
}
