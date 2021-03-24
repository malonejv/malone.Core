//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:40</date>

using malone.Core.Entities.Model;
using System;
using System.Collections.Generic;

namespace malone.Core.Business.Components
{
    /// <summary>
    /// Defines the <see cref="BaseBusinessValidator{TEntity}" />.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    public class BaseBusinessValidator<TEntity> : IBaseBusinessValidator<TEntity>
           where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBusinessValidator{TEntity}"/> class.
        /// </summary>
        public BaseBusinessValidator()
        {
            AddValidationRules = new List<ValidationRule>();
            UpdateValidationRules = new List<ValidationRule>();
            DeleteValidationRules = new List<ValidationRule>();
        }

        /// <summary>
        /// Gets or sets the AddValidationRules.
        /// </summary>
        public List<ValidationRule> AddValidationRules { get; set; }

        /// <summary>
        /// The ExecuteAddValidationRules.
        /// </summary>
        /// <param name="validationRules">The validationRules<see cref="List{ValidationRule}"/>.</param>
        /// <returns>The <see cref="ValidationResultList"/>.</returns>
        public virtual ValidationResultList ExecuteAddValidationRules(List<ValidationRule> validationRules)
        {
            return ExecutesValidationRules(validationRules);
        }

        /// <summary>
        /// Gets or sets the UpdateValidationRules.
        /// </summary>
        public List<ValidationRule> UpdateValidationRules { get; set; }

        /// <summary>
        /// The ExecuteUpdateValidationRules.
        /// </summary>
        /// <param name="validationRules">The validationRules<see cref="List{ValidationRule}"/>.</param>
        /// <returns>The <see cref="ValidationResultList"/>.</returns>
        public virtual ValidationResultList ExecuteUpdateValidationRules(List<ValidationRule> validationRules)
        {
            return ExecutesValidationRules(validationRules);
        }

        /// <summary>
        /// Gets or sets the DeleteValidationRules.
        /// </summary>
        public List<ValidationRule> DeleteValidationRules { get; set; }

        /// <summary>
        /// The ExecuteDeleteValidationRules.
        /// </summary>
        /// <param name="validationRules">The validationRules<see cref="List{ValidationRule}"/>.</param>
        /// <returns>The <see cref="ValidationResultList"/>.</returns>
        public virtual ValidationResultList ExecuteDeleteValidationRules(List<ValidationRule> validationRules)
        {
            return ExecutesValidationRules(validationRules);
        }

        /// <summary>
        /// The Validate.
        /// </summary>
        /// <param name="validationTriggerMethod">The validationTriggerMethod<see cref="ExecuteValidationRulesDelegate"/>.</param>
        /// <param name="validationRules">The validationRules<see cref="List{ValidationRule}"/>.</param>
        /// <returns>The <see cref="ValidationResultList"/>.</returns>
        public ValidationResultList Validate(ExecuteValidationRulesDelegate validationTriggerMethod, List<ValidationRule> validationRules)
        {
            ValidationResultList resultValidations = validationTriggerMethod(validationRules);

            return resultValidations;
        }

        /// <summary>
        /// The ExecutesValidationRules.
        /// </summary>
        /// <param name="validationRules">The validationRules<see cref="List{ValidationRule}"/>.</param>
        /// <returns>The <see cref="ValidationResultList"/>.</returns>
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

        /// <summary>
        /// The InvokeValidationMethod.
        /// </summary>
        /// <param name="rule">The rule<see cref="ValidationRuleDelegate"/>.</param>
        /// <param name="args">The args<see cref="object[]"/>.</param>
        /// <returns>The <see cref="ValidationResult"/>.</returns>
        protected ValidationResult InvokeValidationMethod(ValidationRuleDelegate rule, params object[] args)
        {
            return rule(args);
        }
    }

    /// <summary>
    /// Defines the <see cref="BusinessValidator{TKey, TEntity}" />.
    /// </summary>
    /// <typeparam name="TKey">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    public class BusinessValidator<TKey, TEntity> : BaseBusinessValidator<TEntity>, IBusinessValidator<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessValidator{TKey, TEntity}"/> class.
        /// </summary>
        public BusinessValidator() : base()
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="BusinessValidator{TEntity}" />.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    public class BusinessValidator<TEntity> : BusinessValidator<int, TEntity>, IBusinessValidator<TEntity>
       where TEntity : class, IBaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessValidator{TEntity}"/> class.
        /// </summary>
        public BusinessValidator() : base()
        {
        }
    }
}
