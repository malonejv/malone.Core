//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:41</date>

using System.Collections.Generic;
using System.Linq;

namespace malone.Core.Business.Components
{
    public delegate ValidationResult ValidationRuleDelegate(params object[] arguments);

    public class ValidationRule
    {
        public ValidationRule()
        {
            Arguments = new List<object>();
        }

        public ValidationRuleDelegate Method { get; set; }

        public List<object> Arguments { get; set; }
    }

    public class ValidationResult
    {
        public ValidationResult(string errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public ValidationResult(string errorCode) : this(errorCode, null)
        {
        }

        public object Return { get; set; }

        public bool IsValid { get; set; }

        public string ErrorCode { get; set; }

        public string Message { get; internal protected set; }
    }

    public class ValidationResultList : List<ValidationResult>
    {
        public bool IsValid
        {
            get
            {
                if (this.Count > 0 && this.Any(item => item.IsValid == false))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public ValidationResultList()
        {
        }
    }
}
