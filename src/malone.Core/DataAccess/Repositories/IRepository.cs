//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:13</date>

namespace malone.Core.DataAccess.Repositories
{
	using System;
	using malone.Core.Entities.Model;

	/// <summary>
	/// Defines the <see cref="IRepository{TKey, TEntity}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	public interface IRepository<TKey, TEntity> : IBaseRepository<TEntity>, IQueryRepository<TKey, TEntity>, IDataManipulationRepository<TKey, TEntity>
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
