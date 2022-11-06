//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:13</date>

namespace malone.Core.DataAccess.Repositories
{
	using System;
	using malone.Core.Entities.Model;

	/// <summary>
	/// Defines the <see cref="IRepository{TKey, TEntity}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	public interface IRepository<TKey, TEntity> : IBaseRepository<TEntity>
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
		/// <summary>
		/// The GetById.
		/// </summary>
		/// <param name="id">The id <typeparamref name="TKey">TKey</typeparamref>.</param>
		/// <param name="includeDeleted">The includeDeleted <see cref="bool"/>.</param>
		/// <returns>The <typeparamref name="TEntity">TEntity</typeparamref>.</returns>
		TEntity GetById(
			TKey id,
			bool includeDeleted = false);

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
