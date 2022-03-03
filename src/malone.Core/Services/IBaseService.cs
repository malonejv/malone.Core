using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Filters;

namespace malone.Core.Services
{

	/// <summary>
	/// Defines the <see cref="IBaseService{TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public interface IBaseService<TEntity, TValidator> : IBaseQueryService<TEntity, TValidator>, IBaseDataManipulationService<TEntity, TValidator>
		where TEntity : class
		where TValidator : IBaseServiceValidator<TEntity>
	{
		//IBaseQueryService<TEntity, TValidator> QueryService { get; }
		//IBaseDataManipulationService<TEntity, TValidator> DataManipulationService { get; }
	}

}
