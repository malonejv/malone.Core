//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:41</date>

namespace malone.Core.Services
{
	using System;
	using System.Collections.Generic;
	using malone.Core.Entities.Model;

	/// <summary>
	/// Defines the <see cref="IServiceValidator{TKey, TEntity}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	public interface IServiceValidator<TKey, TEntity> : IBaseServiceValidator<TEntity>
where TKey : IEquatable<TKey>
where TEntity : class, IBaseEntity<TKey>
	{
	}

	/// <summary>
	/// Defines the <see cref="IServiceValidator{TEntity}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	public interface IServiceValidator<TEntity> : IServiceValidator<int, TEntity>
where TEntity : class, IBaseEntity
	{
	}
}
