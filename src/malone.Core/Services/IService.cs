//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:41</date>

namespace malone.Core.Services
{
	using System;
	using malone.Core.Entities.Model;

	/// <summary>
	/// Defines the <see cref="IService{TKey, TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public interface IService<TKey, TEntity, TValidator> : IBaseService<TEntity, TValidator>, IQueryService<TKey, TEntity, TValidator>, ICUDService<TKey, TEntity, TValidator>
where TKey : IEquatable<TKey>
where TEntity : class, IBaseEntity<TKey>
where TValidator : IServiceValidator<TKey, TEntity>
	{
		//new IQueryService<TKey, TEntity, TValidator> QueryService { get; }
		//new ICUDService<TKey, TEntity, TValidator> CUDService { get; }

	}

	/// <summary>
	/// Defines the <see cref="IService{TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public interface IService<TEntity, TValidator> : IService<int, TEntity, TValidator>
where TEntity : class, IBaseEntity
where TValidator : IServiceValidator<TEntity>
	{
	}
}
