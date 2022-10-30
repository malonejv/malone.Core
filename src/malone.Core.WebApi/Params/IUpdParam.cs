using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Entities.Model;

namespace malone.Core.WebApi.Params
{
	/// <summary>
	/// Defines the <see cref="IParam" />.
	/// </summary>
	public interface IUpdParam<TKey, TEntity> : IParam
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
		TKey Id { get; set; }

		TEntity ToEntity(TEntity entity);
	}

	/// <summary>
	/// Defines the <see cref="IParam" />.
	/// </summary>
	public interface IUpdParam<TEntity> : IUpdParam<int, TEntity>
		where TEntity : class, IBaseEntity
	{
	}
}
