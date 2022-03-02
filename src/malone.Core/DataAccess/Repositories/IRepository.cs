//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:13</date>

namespace malone.Core.DataAccess.Repositories
{
	using System;
	using malone.Core.Entities.Model;

	/// <summary>
	/// Defines the <see cref="IRepository{TKey, TEntity}" />.
	/// </summary>
	/// <typeparam name="TKey">.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	public interface IRepository<TKey, TEntity> : IQueryOperationsRepository<TKey, TEntity>, IDataOperationsRepository<TKey, TEntity>
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
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
