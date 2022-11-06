using System;
using System.Collections.Generic;
using System.Linq;
using malone.Core.Entities.Filters;

namespace malone.Core.Services
{

	/// <summary>
	/// Defines the <see cref="IBaseService{TEntity}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	public interface IBaseService<TEntity> 
		where TEntity : class
	{
		/// <summary>
		/// Gets a list of entities.
		/// </summary>
		/// <param name="orderBy">The orderBy<see cref="Func{IQueryable, IOrderedQueryable}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <returns>The <see cref="IEnumerable{TEntity}"/>.</returns>
		IEnumerable<TEntity> GetAll(
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			bool includeDeleted = false
			);

		/// <summary>
		/// Gets an entity.
		/// </summary>
		/// <typeparam name="TFilter">Filter type.</typeparam>
		/// <param name="filter">The filter<typeparamref name="TFilter"/>.</param>
		/// <param name="orderBy">The orderBy<see cref="Func{IQueryable, IOrderedQueryable}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <returns>The <see cref="IEnumerable{TEntity}"/>.</returns>
		IEnumerable<TEntity> Get<TFilter>(
			TFilter filter = default(TFilter),
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			bool includeDeleted = false)
			where TFilter : class, IFilterExpression;

		/// <summary>
		/// Gets an entity.
		/// </summary>
		/// <typeparam name="TFilter">Filter type.</typeparam>
		/// <param name="filter">The filter <typeparamref name="TFilter"/>.</param>
		/// <param name="orderBy">The orderBy <see cref="Func{IQueryable, IOrderedQueryable}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted <see cref="bool"/>.</param>
		/// <returns>The <typeparamref name="TEntity"/>.</returns>
		TEntity GetEntity<TFilter>(
			TFilter filter = default(TFilter),
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			bool includeDeleted = false)
			where TFilter : class, IFilterExpression;

		/// <summary>
		/// Adds an entity of type <see cref="TEntity"/>.
		/// </summary>
		/// <param name="entity">The entity <see cref="TEntity"/>.</param>
		/// <param name="saveChanges">Indicates whether the values should be confirmed and saved<see cref="bool"/>.</param>
		void Add(TEntity entity, bool saveChanges = true);

		/// <summary>
		/// Updates an entity of type <see cref="TEntity"/>.
		/// </summary>
		/// <param name="entity">The entity with new values <typeparamref name="TEntity"/>.</param>
		/// <param name="saveChanges">Indicates whether the values should be confirmed and saved<see cref="bool"/>.</param>
		void Update(TEntity entity, bool saveChanges = true);

		/// <summary>
		/// Deletes an entity of type <see cref="TEntity"/>.
		/// </summary>
		/// <param name="entity">The entity <typeparamref name="TEntity"/>.</param>
		/// <param name="saveChanges">Indicates whether the values should be confirmed and saved<see cref="bool"/>.</param>
		void Delete(TEntity entity, bool saveChanges = true);
	}

}
