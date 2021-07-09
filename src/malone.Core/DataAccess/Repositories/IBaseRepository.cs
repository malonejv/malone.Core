//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:13</date>

using malone.Core.Entities.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.DataAccess.Repositories
{
    /// <summary>
    /// Defines the <see cref="IBaseRepository{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public interface IBaseRepository<T>
        where T : class
    {
        /// <summary>
        /// The Get.
        /// </summary>
        /// <typeparam name="TFilter">.</typeparam>
        /// <param name="filter">The filter<see cref="TFilter"/>.</param>
        /// <param name="orderBy">The orderBy<see cref="Func{IQueryable{T}, IOrderedQueryable{T}}"/>.</param>
        /// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
        /// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
        /// <returns>The <see cref="IEnumerable{TEntity}"/>.</returns>
        IEnumerable<T> Get<TFilter>(
           TFilter filter = default(TFilter),
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           bool includeDeleted = false,
           string includeProperties = "")
            where TFilter : class, IFilterExpression;

        /// <summary>
        /// The GetAll.
        /// </summary>
        /// <param name="orderBy">The orderBy<see cref="Func{IQueryable{T}, IOrderedQueryable{T}}"/>.</param>
        /// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
        /// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
        /// <returns>The <see cref="IEnumerable{TEntity}"/>.</returns>
        IEnumerable<T> GetAll(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = ""
           );

        /// <summary>
        /// The GetEntity.
        /// </summary>
        /// <typeparam name="TFilter">.</typeparam>
        /// <param name="filter">The filter<see cref="TFilter"/>.</param>
        /// <param name="orderBy">The orderBy<see cref="Func{IQueryable{T}, IOrderedQueryable{T}}"/>.</param>
        /// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
        /// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
        /// <returns>The <see cref="T"/>.</returns>
        T GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = "")
            where TFilter : class, IFilterExpression;

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        void Insert(T entity);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        void Delete(T entity);

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="oldValues">The oldValues<see cref="T"/>.</param>
        /// <param name="newValues">The newValues<see cref="T"/>.</param>
        void Update(T oldValues, T newValues);
    }
}
