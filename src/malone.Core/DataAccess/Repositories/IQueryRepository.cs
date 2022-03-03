using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Entities.Model;

namespace malone.Core.DataAccess.Repositories
{
	/// <summary>
	/// Defines the <see cref="IQueryRepository{TKey, TEntity}" />.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TEntity"></typeparam>
	public interface IQueryRepository<in TKey, TEntity> : IBaseQueryRepository<TEntity>
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
		/// <summary>
		/// The GetById.
		/// </summary>
		/// <param name="id">The id<c>TKey</c>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		/// <returns>The <c>TEntity</c>.</returns>
		TEntity GetById(
			TKey id,
			bool includeDeleted = false,
			string includeProperties = "");

	}
}
