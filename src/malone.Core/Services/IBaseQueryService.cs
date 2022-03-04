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
	public interface IBaseQueryService<TEntity, TValidator>
		where TEntity : class
		where TValidator : IBaseServiceValidator<TEntity>
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
		/// <param name="filter">The filter<see cref="TFilter"/>.</param>
		/// <param name="orderBy">The orderBy<see cref="Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		/// <returns>The <see cref="TEntity"/>.</returns>
		TEntity GetEntity<TFilter>(
TFilter filter = default(TFilter),
Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
bool includeDeleted = false,
string includeProperties = "")
where TFilter : class, IFilterExpression;

	}
}
