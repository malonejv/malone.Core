using System;
using malone.Core.Entities.Model;
using malone.Core.Services.Requests;

namespace malone.Core.WebApi.Params
{
	/// <summary>
	/// Defines the <see cref="IParam" />.
	/// </summary>
	public interface IParam
	{
	}

	/// <summary>
	/// Defines the <see cref="IParam" />.
	/// </summary>
	public interface IParam<TKey, TEntity> : IParam
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
		TKey Id { get; set; }

		TEntity ToEntity(TEntity entity = null);
	}

	/// <summary>
	/// Defines the <see cref="IParam" />.
	/// </summary>
	public interface IParam<TEntity> : IParam<int, TEntity>
		where TEntity : class, IBaseEntity
	{
	}
}
