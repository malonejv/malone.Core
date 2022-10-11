using System;
using malone.Core.Entities.Model;

namespace malone.Core.Services
{
	/// <summary>
	/// Defines the <see cref="ICUDService{TKey, TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public interface ICUDService<TKey, TEntity, TValidator> : IBaseCUDService<TEntity, TValidator>
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
		where TValidator : IServiceValidator<TKey, TEntity>
	{
		/// <summary>
		/// The Update.
		/// </summary>
		/// <param name="entity">The entity <c>TEntity</c>.</param>
		/// <param name="saveChanges">The saveChanges<see cref="bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW<see cref="bool"/>.</param>
		void Update(TEntity entity, bool saveChanges = true, bool disposeUoW = true);

		/// <summary>
		/// The Delete.
		/// </summary>
		/// <param name="id">The id <c>TKey</c>.</param>
		/// <param name="saveChanges">The saveChanges <see cref="bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW <see cref="bool"/>.</param>
		void Delete(TKey id, bool saveChanges = true, bool disposeUoW = true);
	}

	/// <summary>
	/// Defines the <see cref="ICUDService{TKey, TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public interface ICUDService<TEntity, TValidator> : ICUDService<int, TEntity, TValidator>
		where TEntity : class, IBaseEntity
		where TValidator : IServiceValidator<TEntity>
	{

	}
}
