//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:13</date>

using System;
using System.Collections.Generic;
using System.Linq;
using malone.Core.Entities.Filters;

namespace malone.Core.DataAccess.Repositories
{
	/// <summary>
	/// Defines the <see cref="IBaseRepository{T}" />.
	/// </summary>
	/// <typeparam name="T">.</typeparam>
	public interface IBaseRepository<T>
		where T : class
	{
		#region Query

		/// <summary>
		/// The Get.
		/// </summary>
		/// <typeparam name="TFilter">Type of filter.</typeparam>
		/// <param name="filter">The filter <typeparamref name="TFilter"/>.</param>
		/// <param name="orderBy">The orderBy <see cref="Func{IQueryable, IOrderedQueryable}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted <see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties <see cref="string"/>.</param>
		/// <returns>The <see cref="IEnumerable{T}"/>.</returns>
		IEnumerable<T> Get<TFilter>(
			TFilter filter = default(TFilter),
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			bool includeDeleted = false,
			string includeProperties = "")
			where TFilter : class, IFilterExpression;

		/// <summary>
		/// The GetAll.
		/// </summary>
		/// <param name="orderBy">The orderBy <see cref="Func{IQueryable, IOrderedQueryable}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted <see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties <see cref="string"/>.</param>
		/// <returns>The <see cref="IEnumerable{T}"/>.</returns>
		IEnumerable<T> GetAll(
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			bool includeDeleted = false,
			string includeProperties = ""
			);

		/// <summary>
		/// The GetEntity.
		/// </summary>
		/// <typeparam name="TFilter">.</typeparam>
		/// <param name="filter">The filter <typeparamref name="TFilter"/>.</param>
		/// <param name="orderBy">The orderBy <see cref="Func{IQueryable, IOrderedQueryable}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted <see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties <see cref="string"/>.</param>
		/// <returns>The <typeparamref name="T"/>.</returns>
		T GetEntity<TFilter>(
			TFilter filter = default(TFilter),
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			bool includeDeleted = false,
			string includeProperties = "")
			where TFilter : class, IFilterExpression;

		#endregion

		#region Data Manipulation

		/// <summary>
		/// The Add action.
		/// </summary>
		/// <param name="entity">The entity <typeparamref name="T"/>.</param>
		void Add(T entity);

		/// <summary>
		/// The Update action.
		/// </summary>
		/// <param name="entity">The entity <typeparamref name="T"/>.</param>
		void Update(T entity);

		/// <summary>
		/// The Delete action.
		/// </summary>
		/// <param name="entity">The entity <typeparamref name="T"/>.</param>
		void Delete(T entity);

		#endregion
	}
}
