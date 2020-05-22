using malone.Core.EL.Model;
using System;
using System.Collections.Generic;

namespace malone.Core.BL.Components.Interfaces
{
    public delegate ValidationResultList ExecuteValidationRulesDelegate(List<ValidationRule> validationRules);

    public interface IBusinessValidator<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {

        List<ValidationRule> AddValidationRules { get; set; }
        ValidationResultList ExecuteAddValidationRules(List<ValidationRule> validationRules);

        List<ValidationRule> UpdateValidationRules { get; set; }
        ValidationResultList ExecuteUpdateValidationRules(List<ValidationRule> validationRules);

        List<ValidationRule> DeleteValidationRules { get; set; }
        ValidationResultList ExecuteDeleteValidationRules(List<ValidationRule> validationRules);

        ValidationResultList Validate(ExecuteValidationRulesDelegate validationTriggerMethod, List<ValidationRule> validationRules);
    }

    public interface IBusinessValidator<TEntity> : IBusinessValidator<int, TEntity>
        where TEntity : class, IBaseEntity
    { }
}
