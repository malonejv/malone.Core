using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Entities.Model;

namespace malone.Core.Services
{
	/// <summary>
	/// Defines the <see cref="IQueryService{TKey, TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public interface IQueryService<TKey, TEntity, TValidator> : IBaseQueryService<TEntity, TValidator>
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
		where TValidator : IServiceValidator<TKey, TEntity>
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
	/// Defines the <see cref="IQueryService{TKey, TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public interface IQueryService<TEntity, TValidator> : IQueryService<int, TEntity, TValidator>
		where TEntity : class, IBaseEntity
		where TValidator : IServiceValidator<TEntity>
	{

	}
}
