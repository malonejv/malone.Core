//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:41</date>

namespace malone.Core.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using malone.Core.DataAccess.Repositories;
	using malone.Core.Entities.Filters;
	using malone.Core.Entities.Model;

	/// <summary>
	/// Defines the <see cref="T: IService{TKey, TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TKey">.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public interface IService<TKey, TEntity, TValidator> : IBaseService<TEntity, TValidator>
where TKey : IEquatable<TKey>
where TEntity : class, IBaseEntity<TKey>
where TValidator : IServiceValidator<TKey, TEntity>
	{
		/// <summary>
		/// Gets or sets the Repository.
		/// </summary>
		new IRepository<TKey, TEntity> Repository { get; set; }

		/// <summary>
		/// The GetById.
		/// </summary>
		/// <param name="id">The id<see cref="T: TKey"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="T: bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="T: string"/>.</param>
		/// <returns>The <see cref="T: TEntity"/>.</returns>
		TEntity GetById(
TKey id,
bool includeDeleted = false,
string includeProperties = "");

		/// <summary>
		/// The Update.
		/// </summary>
		/// <param name="entity">The entity<see cref="T: TEntity"/>.</param>
		/// <param name="saveChanges">The saveChanges<see cref="T: bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW<see cref="T: bool"/>.</param>
		void Update(TEntity entity, bool saveChanges = true, bool disposeUoW = true);

		/// <summary>
		/// The Delete.
		/// </summary>
		/// <param name="id">The id<see cref="T: TKey"/>.</param>
		/// <param name="saveChanges">The saveChanges<see cref="T: bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW<see cref="T: bool"/>.</param>
		void Delete(TKey id, bool saveChanges = true, bool disposeUoW = true);
	}

	/// <summary>
	/// Defines the <see cref="T: IService{TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public interface IService<TEntity, TValidator> : IService<int, TEntity, TValidator>
where TEntity : class, IBaseEntity
where TValidator : IServiceValidator<TEntity>
	{
	}
}
