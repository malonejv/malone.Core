//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:41</date>

namespace malone.Core.Services
{
	using System;
	using malone.Core.Entities.Model;

	/// <summary>
	/// Defines the <see cref="IService{TKey, TEntity}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TEntity">Type of entity.</typeparam>
	public interface IService<TKey, TEntity> : IBaseService<TEntity>
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
		/// <summary>
		/// Gets an entity by its id.
		/// </summary>
		/// <typeparam name="TEntity">.</typeparam>
		TEntity GetById(
			TKey id,
			bool includeDeleted = false);

		///<inheritdoc/>
		new TKey Add(TEntity entity, bool saveChanges = true);

		///<inheritdoc/>
		void Delete(TKey id, bool saveChanges = true);
	}

	///<inheritdoc/>
	public interface IService<TEntity> : IService<int, TEntity>
		where TEntity : class, IBaseEntity
	{
	}
}
