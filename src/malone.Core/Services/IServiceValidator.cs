//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:41</date>

using System;
using System.Collections.Generic;
using malone.Core.Entities.Model;

namespace malone.Core.Services
	{
    public delegate ValidationResultList ExecuteValidationRulesDelegate(List<ValidationRule> validationRules);

    public interface IBaseServiceValidator<TEntity>
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

    public interface IServiceValidator<TKey, TEntity> : IBaseServiceValidator<TEntity>
where TKey : IEquatable<TKey>
where TEntity : class, IBaseEntity<TKey>
    {
    }

    public interface IServiceValidator<TEntity> : IServiceValidator<int, TEntity>
where TEntity : class, IBaseEntity
    {
    }
}
