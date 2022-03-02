using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Entities.Model;

namespace malone.Core.DataAccess.Repositories
{
	/// <summary>
	/// Defines the <see cref="T: IQueryOperationsRepository{TKey, TEntity}" />.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TEntity"></typeparam>
	public interface IQueryOperationsRepository<TKey, TEntity> : IBaseQueryOperationsRepository<TEntity>
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
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

	}
}
