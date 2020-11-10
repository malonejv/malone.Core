using malone.Core.Entities.Model;
using System;
using System.Collections.Generic;

namespace malone.Core.Business.Components
{
    public delegate ValidationResultList ExecuteValidationRulesDelegate(List<ValidationRule> validationRules);
    public interface IBaseBusinessValidator<TEntity>
        where TEntity : class
    {

        List<ValidationRule> AddValidationRules { get; set; }
        ValidationResultList ExecuteAddValidationRules(List<ValidationRule> validationRules);

        List<ValidationRule> UpdateValidationRules { get; set; }
        ValidationResultList ExecuteUpdateValidationRules(List<ValidationRule> validationRules);

        List<ValidationRule> DeleteValidationRules { get; set; }
        ValidationResultList ExecuteDeleteValidationRules(List<ValidationRule> validationRules);

        ValidationResultList Validate(ExecuteValidationRulesDelegate validationTriggerMethod, List<ValidationRule> validationRules);
    }

    public interface IBusinessValidator<TKey, TEntity> : IBaseBusinessValidator<TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {

    }

    public interface IBusinessValidator<TEntity> : IBusinessValidator<int, TEntity>
        where TEntity : class, IBaseEntity
    { }
}
