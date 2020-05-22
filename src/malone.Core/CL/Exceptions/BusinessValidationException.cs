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
    public class BusinessValidationException<TCode> : BaseException<TCode>
        where TCode : Enum
    {
        [DefaultValue(null)]
        public ValidationResultList Reuslts { get; private set; }

        [DefaultValue(false)]
        public bool HideErrorCodes { get; protected set; }

        public new string Message
        {
            get
            {
                StringBuilder msg = new StringBuilder();
                if (Reuslts != null)
                {
                    foreach (var e in Reuslts)
                    {
                        msg.AppendLine(string.Format("[{0}] - {1}",e.ErrorCode.ToUpper(), e.Message));
                    }
                }
                return msg.ToString();
            }
        }

        public BusinessValidationException()
            : base()
        {
            Rethrow = true;
        }

        public BusinessValidationException(ValidationResultList results,bool hideErrorCode = false)
        {
            Rethrow = true;
            Reuslts = results;
            HideErrorCodes = hideErrorCode;
        }
        public BusinessValidationException(ValidationResultList errors, bool rethrow, bool hideErrorCode = false)
            : this(errors,hideErrorCode)
        {
            Rethrow = rethrow;
        }

    }

    public class BusinessValidationException : BusinessValidationException<CoreErrors>
    {
        [DefaultValue(null)]
        public List<ValidationResult> Errors { get; private set; }

        public new string Message
        {
            get
            {
                StringBuilder msg = new StringBuilder();
                if (Errors != null)
                {
                    foreach (var e in Errors)
                    {
                        msg.AppendLine(string.Format("[{0}] - {1}", e.ErrorCode.ToUpper(), e.Message));
                    }
                }
                return msg.ToString();
            }
        }

        public BusinessValidationException()
            : base()
        {
            Rethrow = true;
        }

        public BusinessValidationException(List<ValidationResult> errors)
        {
            Rethrow = true;
            Errors = errors;
        }
        public BusinessValidationException(List<ValidationResult> errors, bool rethrow)
            : this(errors)
        {
            Errors = errors;
            Rethrow = rethrow;
        }

    }
}
