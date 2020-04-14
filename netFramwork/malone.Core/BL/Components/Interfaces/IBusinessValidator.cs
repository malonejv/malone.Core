using malone.Core.EL.Model;
using System.Collections.Generic;

namespace malone.Core.BL.Components.Interfaces
{
    public delegate ValidationFailure ValidationRuleDelegate(params object[] arguments);

    public class ValidationRule
    {
        public ValidationRule()
        {
            Arguments = new List<object>();
        }

        public ValidationRuleDelegate Method { get; set; }

        public List<object> Arguments { get; set; }

    }

    public class ValidationFailure
    {
        public ValidationFailure(int? errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public ValidationFailure(int? errorCode) : this(errorCode, null) { }

        public ValidationFailure(string message) : this(null, message) { }

        public int? ErrorCode { get; private set; }
        public string Message { get; private set; }
    }

    public class ValidationResult
    {
        public bool IsValid
        {
            get
            {
                if (Errors != null && Errors.Count > 0)
                    return false;
                else
                    return true;
            }
        }

        public List<ValidationFailure> Errors { get; internal set; }

        public ValidationResult()
        {
            Errors = new List<ValidationFailure>();
        }
    }

    public interface IBusinessValidator<TKey, TEntity>
        where TEntity : class, IBaseEntity<TKey>
    {

        List<ValidationRule> AddValidationRules { get; set; }
        List<ValidationRule> UpdateValidationRules { get; set; }
        List<ValidationRule> DeleteValidationRules { get; set; }

        ValidationResult Validate(List<ValidationRule> validationRules);
    }

    public interface IBusinessValidator<TEntity> : IBusinessValidator<int, TEntity>
        where TEntity : class, IBaseEntity
    { }
}
