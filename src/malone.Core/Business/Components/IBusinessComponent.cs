//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:41</date>

using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.Business.Components
{
    /// <summary>
    /// Defines the <see cref="IBaseBusinessComponent{TEntity, TValidator}" />.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    /// <typeparam name="TValidator">.</typeparam>
    public interface IBaseBusinessComponent<TEntity, TValidator>
        where TEntity : class
        where TValidator : IBaseBusinessValidator<TEntity>
    {
        /// <summary>
        /// Gets or sets the BusinessValidator.
        /// </summary>
        TValidator BusinessValidator { get; set; }

        /// <summary>
        /// Gets or sets the Repository.
        /// </summary>
        IBaseRepository<TEntity> Repository { get; set; }

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

    /// <summary>
    /// Defines the <see cref="IBusinessComponent{TKey, TEntity, TValidator}" />.
    /// </summary>
    /// <typeparam name="TKey">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    /// <typeparam name="TValidator">.</typeparam>
    public interface IBusinessComponent<TKey, TEntity, TValidator> : IBaseBusinessComponent<TEntity, TValidator>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
        where TValidator : IBusinessValidator<TKey, TEntity>
    {
        /// <summary>
        /// Gets or sets the Repository.
        /// </summary>
        new IRepository<TKey, TEntity> Repository { get; set; }

        /// <summary>
        /// The GetById.
        /// </summary>
        /// <param name="id">The id<see cref="TKey"/>.</param>
        /// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
        /// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
        /// <returns>The <see cref="TEntity"/>.</returns>
        TEntity GetById(
            TKey id,
            bool includeDeleted = false,
            string includeProperties = "");

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        /// <param name="saveChanges">The saveChanges<see cref="bool"/>.</param>
        /// <param name="disposeUoW">The disposeUoW<see cref="bool"/>.</param>
        void Update(TEntity entity, bool saveChanges = true, bool disposeUoW = true);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="id">The id<see cref="TKey"/>.</param>
        /// <param name="saveChanges">The saveChanges<see cref="bool"/>.</param>
        /// <param name="disposeUoW">The disposeUoW<see cref="bool"/>.</param>
        void Delete(TKey id, bool saveChanges = true, bool disposeUoW = true);
    }

    /// <summary>
    /// Defines the <see cref="IBusinessComponent{TEntity, TValidator}" />.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    /// <typeparam name="TValidator">.</typeparam>
    public interface IBusinessComponent<TEntity, TValidator> : IBusinessComponent<int, TEntity, TValidator>
        where TEntity : class, IBaseEntity
        where TValidator : IBusinessValidator<TEntity>
    {
    }
}
