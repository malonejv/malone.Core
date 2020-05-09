using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions
{
    public class BusinessValidationException : BaseException
    {
        [DefaultValue(null)]
        public List<string> Errors { get; private set; }

        public new string Message
        {
            get
            {
                StringBuilder msg = new StringBuilder();
                if (Errors != null)
                {
                    foreach (var e in Errors)
                    {
                        msg.AppendLine(e);
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

        public BusinessValidationException(List<string> errors)
        {
            Rethrow = true;
            Errors = errors;
        }
        public BusinessValidationException(List<string> errors, bool rethrow)
            : this(errors)
        {
            Errors = errors;
            Rethrow = rethrow;
        }

    }
}
