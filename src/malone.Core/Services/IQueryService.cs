using System;
using malone.Core.Entities.Model;

namespace malone.Core.Services
{
	/// <summary>
	/// Defines the <see cref="IQueryService{TKey, TEntity}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	public interface IQueryService<TKey, TEntity> : IBaseQueryService<TEntity>
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
		/// <summary>
		/// The GetById.
		/// </summary>
		/// <param name="id">The id <c>TKey</c>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		/// <returns>The <c>TEntity</c>.</returns>
		TEntity GetById(
			TKey id,
			bool includeDeleted = false,
			string includeProperties = "");

	}

	/// <summary>
	/// Defines the <see cref="IQueryService{TKey, TEntity}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	public interface IQueryService<TEntity> : IQueryService<int, TEntity>
		where TEntity : class, IBaseEntity
	{

	}
}
