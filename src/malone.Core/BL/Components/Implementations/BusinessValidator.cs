using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.CL.Exceptions.Manager;
using malone.Core.EL.Model;
using System;
using System.Collections.Generic;

namespace malone.Core.BL.Components.Implementations
{
    //TODO: Estudiar dejarlo como abstract para oblicar a escribir las reglas de validacion aca
    public class BusinessValidator<TKey, TEntity, TErrorCoder> : IBusinessValidator<TKey, TEntity, TErrorCoder>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
        where TErrorCoder : Enum
    {
        protected IExceptionHandler<TErrorCoder> ExceptionHandler { get; }
        protected IMessageHandler<TErrorCoder> MessageHandler { get; }

        public BusinessValidator(IMessageHandler<TErrorCoder> messageHandler, IExceptionHandler<TErrorCoder> exceptionHandler)
        {
            MessageHandler = messageHandler;
            ExceptionHandler = exceptionHandler;
            AddValidationRules = new List<ValidationRule>();
            UpdateValidationRules = new List<ValidationRule>();
            DeleteValidationRules = new List<ValidationRule>();
        }

        public List<ValidationRule> AddValidationRules { get; set; }
        public virtual ValidationResultList ExecuteAddValidationRules(List<ValidationRule> validationRules)
        {
            return DefaultValidationExecution(validationRules);
        }

        public List<ValidationRule> UpdateValidationRules { get; set; }
        public virtual ValidationResultList ExecuteUpdateValidationRules(List<ValidationRule> validationRules)
        {
            return DefaultValidationExecution(validationRules);
        }

        public List<ValidationRule> DeleteValidationRules { get; set; }
        public virtual ValidationResultList ExecuteDeleteValidationRules(List<ValidationRule> validationRules)
        {
            return DefaultValidationExecution(validationRules);
        }

        public ValidationResultList Validate(ExecuteValidationRulesDelegate validationTriggerMethod, List<ValidationRule> validationRules)
        {
            ValidationResultList resultValidations = validationTriggerMethod(validationRules);

            return resultValidations;
        }


        protected ValidationResultList DefaultValidationExecution(List<ValidationRule> validationRules)
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

    public class BusinessValidator<TEntity, TErrorCoder> : BusinessValidator<int, TEntity, TErrorCoder>, IBusinessValidator<TEntity, TErrorCoder>
       where TEntity : class, IBaseEntity
       where TErrorCoder : Enum
    {

        public BusinessValidator(IMessageHandler<TErrorCoder> messageHandler, IExceptionHandler<TErrorCoder> exceptionHandler) : base(messageHandler, exceptionHandler)
        {
        }

    }
}
