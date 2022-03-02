using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Entities.Model;

namespace malone.Core.DataAccess.Repositories
{
	public interface IDataOperationsRepository<TKey, TEntity> : IBaseDataOperationsRepository<TEntity>
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
		///// <summary>
		///// The Update.
		///// </summary>
		///// <param name="entity">The entity<see cref="TEntity"/>.</param>
		//void Update(TEntity entity);
	}
}
