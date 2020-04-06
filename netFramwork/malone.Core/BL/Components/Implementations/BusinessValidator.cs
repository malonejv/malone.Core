using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler.Interfaces;
using malone.Core.CL.Exceptions.Manager.Interfaces;
using malone.Core.EL;

namespace malone.Core.BL.Components.Implementations
{
    public class BusinessValidator<TKey, TEntity> : IBusinessValidator<TKey, TEntity>
        where TEntity : class, IBaseEntity<TKey>
    {

        protected IExceptionMessageManager MessageManager { get; }

        protected IExceptionHandler ExceptionHandler { get; }

        public BusinessValidator(IExceptionMessageManager exManager, IExceptionHandler exHandler)
        {
            AddValidationRules = new List<ValidationRule>();
            UpdateValidationRules = new List<ValidationRule>();
            DeleteValidationRules = new List<ValidationRule>();

            MessageManager = exManager;
            ExceptionHandler = exHandler;
        }

        public List<ValidationRule> AddValidationRules { get; set; }
        public List<ValidationRule> UpdateValidationRules { get; set; }
        public List<ValidationRule> DeleteValidationRules { get; set; }

        public ValidationResult Validate(List<ValidationRule> validationRules)
        {
            ValidationResult resultValidations = new ValidationResult();
            ValidationFailure result = null;

            foreach (var validation in validationRules)
            {
                result = InvokeValidationMethod(validation.Method, validation.Arguments.ToArray());

                if (result != null)
                    resultValidations.Errors.Add(result);

                result = null;
            }
            return resultValidations;
        }

        protected ValidationFailure InvokeValidationMethod(ValidationRuleDelegate rule, params object[] args)
        {
            return rule(args);
        }
    }

    public class BusinessValidator<TEntity> : BusinessValidator<int, TEntity>, IBusinessValidator<TEntity>
       where TEntity : class, IBaseEntity
    {

        public BusinessValidator(IExceptionMessageManager exManager, IExceptionHandler exHandler) : base(exManager, exHandler)
        {
        }

    }
}
