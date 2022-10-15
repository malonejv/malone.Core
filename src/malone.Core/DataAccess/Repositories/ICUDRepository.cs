using System;
using malone.Core.Entities.Model;

namespace malone.Core.DataAccess.Repositories
{
	public interface ICUDRepository<TKey, TEntity> : IBaseCUDRepository<TEntity>
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
		///// <summary>
		///// The Update.
		///// </summary>
		///// <param name="entity">The entity<see cref="TEntity"/>.</param>
		//void Update(TEntity entity);
	}
	public interface ICUDRepository<TEntity> : ICUDRepository<int, TEntity>
		where TEntity : class, IBaseEntity<int>
	{
	}
}
