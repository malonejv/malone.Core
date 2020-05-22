using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions
{
    public abstract class BaseException<TCode> : Exception
        where TCode : Enum
    {
        [DefaultValue(true)]
        public bool Rethrow { get; protected set; }

        [DefaultValue(true)]
        public bool ShouldLog { get; protected set; }

        public TCode Error { get; private set; }

        public string CodeMessage { get { return Error.ToString() + " - " + Message; } }

        public BaseException()
            : base()
        {
        }

        public BaseException(TCode code, string message)
            : base(message)
        {
            Error = code;
        }

        public BaseException(TCode code, string message, bool rethrow)
        {
            Error = code;
        }

        public BaseException(TCode code, string message, Exception innerException)
        {
            Error = code;
        }

        public BaseException(TCode code, string message, Exception innerException, bool rethrow)
        {
            Error = code;
        }

    }
}
