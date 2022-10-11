using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Services
{
	/// <summary>
	/// The ExecuteValidationRulesDelegate.
	/// </summary>
	/// <param name="validationRules">The validationRules<see cref="List{ValidationRule}"/>.</param>
	/// <returns>The <see cref="ValidationResultList"/>.</returns>
	public delegate ValidationResultList ExecuteValidationRulesDelegate(List<ValidationRule> validationRules);

	/// <summary>
	/// Defines the <see cref="IBaseServiceValidator{TEntity}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	public interface IBaseServiceValidator<TEntity>
where TEntity : class
	{
		/// <summary>
		/// Gets or sets the AddValidationRules.
		/// </summary>
		List<ValidationRule> AddValidationRules { get; set; }

		/// <summary>
		/// The ExecuteAddValidationRules.
		/// </summary>
		/// <param name="validationRules">The validationRules<see cref="List{ValidationRule}"/>.</param>
		/// <returns>The <see cref="ValidationResultList"/>.</returns>
		ValidationResultList ExecuteAddValidationRules(List<ValidationRule> validationRules);

		/// <summary>
		/// Gets or sets the UpdateValidationRules.
		/// </summary>
		List<ValidationRule> UpdateValidationRules { get; set; }

		/// <summary>
		/// The ExecuteUpdateValidationRules.
		/// </summary>
		/// <param name="validationRules">The validationRules<see cref="List{ValidationRule}"/>.</param>
		/// <returns>The <see cref="ValidationResultList"/>.</returns>
		ValidationResultList ExecuteUpdateValidationRules(List<ValidationRule> validationRules);

		/// <summary>
		/// Gets or sets the DeleteValidationRules.
		/// </summary>
		List<ValidationRule> DeleteValidationRules { get; set; }

		/// <summary>
		/// The ExecuteDeleteValidationRules.
		/// </summary>
		/// <param name="validationRules">The validationRules<see cref="List{ValidationRule}"/>.</param>
		/// <returns>The <see cref="ValidationResultList"/>.</returns>
		ValidationResultList ExecuteDeleteValidationRules(List<ValidationRule> validationRules);

		/// <summary>
		/// The Validate.
		/// </summary>
		/// <param name="validationTriggerMethod">The validationTriggerMethod<see cref="ExecuteValidationRulesDelegate"/>.</param>
		/// <param name="validationRules">The validationRules<see cref="List{ValidationRule}"/>.</param>
		/// <returns>The <see cref="ValidationResultList"/>.</returns>
		ValidationResultList Validate(ExecuteValidationRulesDelegate validationTriggerMethod, List<ValidationRule> validationRules);
	}

}
