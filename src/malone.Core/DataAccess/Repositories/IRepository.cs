//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:13</date>

using malone.Core.Entities.Model;
using System;

namespace malone.Core.DataAccess.Repositories
{
    /// <summary>
    /// Defines the <see cref="IRepository{TKey, TEntity}" />.
    /// </summary>
    /// <typeparam name="TKey">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    public interface IRepository<TKey, TEntity> : IBaseRepository<TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
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
        void Update(TEntity entity);
    }

    /// <summary>
    /// Defines the <see cref="IRepository{TEntity}" />.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    public interface IRepository<TEntity> : IRepository<int, TEntity>
        where TEntity : class, IBaseEntity
    {
    }
}
