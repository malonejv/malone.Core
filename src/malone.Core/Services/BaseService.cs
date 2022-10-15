using System;
using System.Collections.Generic;
using System.Linq;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Filters;
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
		protected IBaseQueryService<TEntity> QueryService { get; private set; }

		/// <summary>
		/// Gets or sets the CUDRepository.
		/// </summary>
		protected IBaseCUDService<TEntity, TValidator> CUDService { get; private set; }

		/// <summary>
		/// Gets or sets the ServiceValidator.
		/// </summary>
		protected TValidator ServiceValidator { get; private set; }

		/// <summary>
		/// Gets or sets the Logger.
		/// </summary>
		protected ICoreLogger Logger { get; private set; }

		#region Constructor 

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseService{TEntity, TValidator}"/> class.
		/// </summary>
		/// <param name="validator">The validator <typeparamref name="TValidator"/>.</param>
		/// <param name="logger">The logger <see cref="ICoreLogger"/>.</param>
		/// <param name="queryRepository">The queryRepository <see cref="IBaseQueryRepository{TEntity}"/>.</param>
		/// <param name="cudRepository">The cudRepository <see cref="IBaseCUDRepository{TEntity}"/>.</param>
		protected BaseService(TValidator validator, ICoreLogger logger, IBaseQueryRepository<TEntity> queryRepository, IBaseCUDRepository<TEntity> cudRepository)
			:this(validator,logger)
		{
			QueryService = new BaseQueryService<TEntity>(queryRepository, logger);
			CUDService = new BaseCUDService<TEntity, TValidator>(validator, cudRepository, logger);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseService{TEntity, TValidator}"/> class.
		/// </summary>
		/// <param name="validator">The validator <typeparamref name="TValidator"/>.</param>
		/// <param name="logger">The logger <see cref="ICoreLogger"/>.</param>
		protected BaseService(TValidator validator, ICoreLogger logger)
		{
			ServiceValidator = validator.ThrowIfNull();
			Logger = logger.ThrowIfNull();
		}

		#endregion

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

		public virtual void Add(TEntity entity, bool saveChanges = true, bool disposeUoW = true)
		{
			CUDService.Add(entity, saveChanges, disposeUoW);
		}

		public virtual void Update(TEntity oldValues, TEntity newValues, bool saveChanges = true, bool disposeUoW = true)
		{
			CUDService.Update(oldValues, newValues, saveChanges, disposeUoW);
		}

		public virtual void Delete(TEntity entity, bool saveChanges = true, bool disposeUoW = true)
		{
			CUDService.Delete(entity, saveChanges, disposeUoW);
		}
	}

}
