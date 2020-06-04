using malone.Core.BL.Components.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions
{
    /// <summary>
    /// Encapsulates funcional validation exceptions in Business Layer
    /// </summary>
    public class BusinessValidationException : BaseException
    {
        public ValidationResultList Results { get; private set; }

        public bool HideErrorCodes { get; protected set; }

        public new string Message
        {
            get
            {
                StringBuilder msg = new StringBuilder();
                if (Results != null)
                {
                    foreach (var e in Results)
                    {
                        msg.AppendLine(string.Format("[{0}] - {1}", e.ErrorCode.ToUpper(), e.Message));
                    }
                }
                return msg.ToString();
            }
        }


        public BusinessValidationException()
            : base() { }

        public BusinessValidationException(string message, bool rethrow = false, bool shouldLog = true)
            : base(message, rethrow, shouldLog) { }

        public BusinessValidationException(string message, Exception innerException, bool rethrow = false, bool shouldLog = true)
            : base(message, innerException, rethrow, shouldLog) { }


        public BusinessValidationException(ValidationResultList results, bool rethrow = false, bool shouldLog = true, bool hideErrorCode = false)
            :base(rethrow,shouldLog)
        {
            Results = results;
            HideErrorCodes = hideErrorCode;
        }
    }

}
