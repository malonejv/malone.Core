//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:40</date>

using System;
using System.Collections.Generic;
using malone.Core.Entities.Model;

namespace malone.Core.Services
{
    public class BaseServiceValidator<TEntity> : IBaseServiceValidator<TEntity>
           where TEntity : class
    {
        public BaseServiceValidator()
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
                {
                    resultValidations.Add(result);
                }

                result = null;
            }
            return resultValidations;
        }

        protected ValidationResult InvokeValidationMethod(ValidationRuleDelegate rule, params object[] args)
        {
            return rule(args);
        }
    }

    public class ServiceValidator<TKey, TEntity> : BaseServiceValidator<TEntity>, IServiceValidator<TKey, TEntity>
where TKey : IEquatable<TKey>
where TEntity : class, IBaseEntity<TKey>
    {
        public ServiceValidator() : base()
        {
        }
    }

    public class ServiceValidator<TEntity> : ServiceValidator<int, TEntity>, IServiceValidator<TEntity>
where TEntity : class, IBaseEntity
    {
        public ServiceValidator() : base()
        {
        }
    }
}
