//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:13</date>

using malone.Core.Entities.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.DataAccess.Repositories
{
    /// <summary>
    /// Defines the <see cref="IBaseRepository{TEntity}" />.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
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

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="oldValues">The oldValues<see cref="TEntity"/>.</param>
        /// <param name="newValues">The newValues<see cref="TEntity"/>.</param>
        void Update(TEntity oldValues, TEntity newValues);
    }
}
