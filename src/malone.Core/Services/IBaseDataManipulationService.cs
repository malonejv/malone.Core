using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Filters;

namespace malone.Core.Services
{
	/// <summary>
	/// Defines the <see cref="IBaseService{TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public interface IBaseDataManipulationService<TEntity, TValidator>
		where TEntity : class
		where TValidator : IBaseServiceValidator<TEntity>
	{
		/// <summary>
		/// The Add.
		/// </summary>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		/// <param name="saveChanges">The saveChanges<see cref="bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW<see cref="bool"/>.</param>
		void Add(TEntity entity, bool saveChanges = true, bool disposeUoW = true);

		/// <summary>
		/// The Update.
		/// </summary>
		/// <param name="oldValues">The oldValues<see cref="TEntity"/>.</param>
		/// <param name="newValues">The newValues<see cref="TEntity"/>.</param>
		/// <param name="saveChanges">The saveChanges<see cref="bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW<see cref="bool"/>.</param>
		void Update(TEntity oldValues, TEntity newValues, bool saveChanges = true, bool disposeUoW = true);

		/// <summary>
		/// The Delete.
		/// </summary>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		/// <param name="saveChanges">The saveChanges<see cref="bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW<see cref="bool"/>.</param>
		void Delete(TEntity entity, bool saveChanges = true, bool disposeUoW = true);
	}
}
