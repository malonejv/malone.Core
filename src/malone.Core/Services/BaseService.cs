using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;
using malone.Core.Logging;

namespace malone.Core.Services
{

	/// <summary>
	/// Defines the <see cref="BaseService{TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public abstract class BaseService<TEntity, TValidator> : IBaseService<TEntity, TValidator>
		where TEntity : class
		where TValidator : IBaseServiceValidator<TEntity>
	{
		/// <summary>
		/// Gets or sets the QueryRepository.
		/// </summary>
		private IBaseQueryService<TEntity, TValidator> QueryService { get; }

		/// <summary>
		/// Gets or sets the DataManipulationRepository.
		/// </summary>
		private IBaseDataManipulationService<TEntity, TValidator> DataManipulationService { get; }

		/// <summary>
		/// Gets or sets the ServiceValidator.
		/// </summary>
		public TValidator ServiceValidator { get; set; }

		/// <summary>
		/// Gets or sets the Logger.
		/// </summary>
		public ICoreLogger Logger { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseService{TEntity, TValidator}"/> class.
		/// </summary>
		/// <param name="businessValidator">The businessValidator<see cref="TValidator"/>.</param>
		/// <param name="queryRepository">The repository<see cref="IBaseQueryRepository{TEntity}"/>.</param>
		/// <param name="dataManipulationRepository">The repository<see cref="IBaseDataManipulationRepository{TEntity}"/>.</param>
		/// <param name="logger">The logger<see cref="ICoreLogger"/>.</param>
		protected BaseService(TValidator businessValidator, IBaseQueryService<TEntity, TValidator> queryService, IBaseDataManipulationService<TEntity, TValidator> dataManipulationService, ICoreLogger logger)
		{
			ServiceValidator = businessValidator.ThrowIfNull();
			QueryService = queryService.ThrowIfNull();
			DataManipulationService = dataManipulationService.ThrowIfNull();
			Logger = logger.ThrowIfNull();
		}


		public IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeDeleted = false, string includeProperties = "")
		{
			return QueryService.GetAll(orderBy, includeDeleted, includeProperties);
		}

		public IEnumerable<TEntity> Get<TFilter>(TFilter filter = default(TFilter), Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeDeleted = false,
			string includeProperties = "") where TFilter : class, IFilterExpression
		{
			return QueryService.Get(filter, orderBy, includeDeleted, includeProperties);
		}

		public TEntity GetEntity<TFilter>(TFilter filter = default(TFilter), Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeDeleted = false,
			string includeProperties = "") where TFilter : class, IFilterExpression
		{
			return QueryService.GetEntity(filter, orderBy, includeDeleted, includeProperties);
		}

		public void Add(TEntity entity, bool saveChanges = true, bool disposeUoW = true)
		{
			DataManipulationService.Add(entity, saveChanges, disposeUoW);
		}

		public void Update(TEntity oldValues, TEntity newValues, bool saveChanges = true, bool disposeUoW = true)
		{
			DataManipulationService.Update(oldValues, newValues, saveChanges, disposeUoW);
		}

		public void Delete(TEntity entity, bool saveChanges = true, bool disposeUoW = true)
		{
			DataManipulationService.Delete(entity, saveChanges, disposeUoW);
		}
	}

}
