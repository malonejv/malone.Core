﻿namespace malone.Core.Services
{

	/// <summary>
	/// Defines the <see cref="IBaseService{TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public interface IBaseService<TEntity, TValidator> : IBaseQueryService<TEntity, TValidator>, IBaseCUDService<TEntity, TValidator>
		where TEntity : class
		where TValidator : IBaseServiceValidator<TEntity>
	{
		//IBaseQueryService<TEntity, TValidator> QueryService { get; }
		//IBaseCUDService<TEntity, TValidator> CUDService { get; }
	}

}
