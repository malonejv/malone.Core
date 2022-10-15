using System;
using System.Collections.Generic;
using System.Linq;
using malone.Core.Entities.Filters;

namespace malone.Core.Services
{
	/// <summary>
	/// Defines the <see cref="IBaseService{TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	public interface IBaseQueryService<TEntity>
		where TEntity : class
	{
		/// <summary>
		/// The GetAll.
		/// </summary>
		/// <param name="orderBy">The orderBy<see cref="Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		/// <returns>The <see cref="IEnumerable{TEntity}"/>.</returns>
		IEnumerable<TEntity> GetAll(
Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
bool includeDeleted = false,
string includeProperties = ""
);

		/// <summary>
		/// The Get.
		/// </summary>
		/// <typeparam name="TFilter">.</typeparam>
		/// <param name="filter">The filter<see cref="TFilter"/>.</param>
		/// <param name="orderBy">The orderBy<see cref="Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		/// <returns>The <see cref="IEnumerable{TEntity}"/>.</returns>
		IEnumerable<TEntity> Get<TFilter>(
TFilter filter = default(TFilter),
Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
bool includeDeleted = false,
string includeProperties = "")
where TFilter : class, IFilterExpression;

		/// <summary>
		/// The GetEntity.
		/// </summary>
		/// <typeparam name="TFilter">.</typeparam>
		/// <param name="filter">The filter <typeparamref name="TFilter"/>.</param>
		/// <param name="orderBy">The orderBy <see cref="Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		/// <returns>The <typeparamref name="TEntity"/>.</returns>
		TEntity GetEntity<TFilter>(
TFilter filter = default(TFilter),
Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
bool includeDeleted = false,
string includeProperties = "")
where TFilter : class, IFilterExpression;

	}
}
