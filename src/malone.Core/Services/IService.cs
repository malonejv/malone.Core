//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:41</date>

namespace malone.Core.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using malone.Core.DataAccess.Repositories;
	using malone.Core.Entities.Filters;
	using malone.Core.Entities.Model;

	/// <summary>
	/// Defines the <see cref="IService{TKey, TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public interface IService<TKey, TEntity, TValidator> : IBaseService<TEntity, TValidator>, IQueryService<TKey, TEntity, TValidator>, IDataManipulationService<TKey, TEntity, TValidator>
where TKey : IEquatable<TKey>
where TEntity : class, IBaseEntity<TKey>
where TValidator : IServiceValidator<TKey, TEntity>
	{
		//new IQueryService<TKey, TEntity, TValidator> QueryService { get; }
		//new IDataManipulationService<TKey, TEntity, TValidator> DataManipulationService { get; }

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
