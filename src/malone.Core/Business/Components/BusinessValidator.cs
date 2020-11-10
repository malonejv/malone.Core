using malone.Core.Entities.Model;
using System;
using System.Collections.Generic;

namespace malone.Core.Business.Components
{
    public class BaseBusinessValidator<TEntity> : IBaseBusinessValidator<TEntity>
           where TEntity : class
    {
        public BaseBusinessValidator()
        {
            AddValidationRules = new List<ValidationRule>();
            UpdateValidationRules = new List<ValidationRule>();
            DeleteValidationRules = new List<ValidationRule>();
        }

        public List<ValidationRule> AddValidationRules { get; set; }
        public virtual ValidationResultList ExecuteAddValidationRules(List<ValidationRule> validationRules)
        {
            return ExecutesValidationRules(validationRules);
        }

        public List<ValidationRule> UpdateValidationRules { get; set; }
        public virtual ValidationResultList ExecuteUpdateValidationRules(List<ValidationRule> validationRules)
        {
            return ExecutesValidationRules(validationRules);
        }

        public List<ValidationRule> DeleteValidationRules { get; set; }
        public virtual ValidationResultList ExecuteDeleteValidationRules(List<ValidationRule> validationRules)
        {
            return ExecutesValidationRules(validationRules);
        }

        public ValidationResultList Validate(ExecuteValidationRulesDelegate validationTriggerMethod, List<ValidationRule> validationRules)
        {
            ValidationResultList resultValidations = validationTriggerMethod(validationRules);

            return resultValidations;
        }

        protected ValidationResultList ExecutesValidationRules(List<ValidationRule> validationRules)
        {
            ValidationResultList resultValidations = new ValidationResultList();
            ValidationResult result = null;

            foreach (var validation in validationRules)
            {
                result = InvokeValidationMethod(validation.Method, validation.Arguments.ToArray());

                if (result != null)
                    resultValidations.Add(result);

                result = null;
            }
            return resultValidations;
        }
        protected ValidationResult InvokeValidationMethod(ValidationRuleDelegate rule, params object[] args)
        {
            return rule(args);
        }
    }
    //TODO: Estudiar dejarlo como abstract para obligar a escribir las reglas de validacion aca
    public class BusinessValidator<TKey, TEntity> : BaseBusinessValidator<TEntity>, IBusinessValidator<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        public BusinessValidator() : base()
        {
        }
    }

    public class BusinessValidator<TEntity> : BusinessValidator<int, TEntity>, IBusinessValidator<TEntity>
       where TEntity : class, IBaseEntity
    {
        public BusinessValidator() : base()
        {
        }
    }
}
