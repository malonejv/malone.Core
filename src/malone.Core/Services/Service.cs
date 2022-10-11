//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:39</date>

namespace malone.Core.Services
{
	using System;
	using malone.Core.DataAccess.Repositories;
	using malone.Core.Entities.Model;
	using malone.Core.Logging;

	/// <summary>
	/// Defines the <see cref="Service{TKey, TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public class Service<TKey, TEntity, TValidator> : BaseService<TEntity, TValidator>,
		IService<TKey, TEntity, TValidator>
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
		where TValidator : IServiceValidator<TKey, TEntity>
	{
		private IQueryService<TKey, TEntity, TValidator> QueryService { get; }
		private ICUDService<TKey, TEntity, TValidator> CUDService { get; }

		/// <summary>
		/// Gets or sets the Logger.
		/// </summary>
		public ICoreLogger Logger { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Service{TKey, TEntity, TValidator}"/> class.
		/// </summary>
		/// <param name="validator">The validator<see cref="TValidator"/>.</param>
		/// <param name="repository">The repository<see cref="IRepository{TKey, TEntity}"/>.</param>
		/// <param name="logger">The logger<see cref="ICoreLogger"/>.</param>
		public Service(TValidator validator, IQueryService<TKey, TEntity, TValidator> queryService,
			ICUDService<TKey, TEntity, TValidator> cudService, ICoreLogger logger) :
			base(validator, queryService, cudService, logger)
		{
			ServiceValidator = validator;
			QueryService = queryService;
			CUDService = cudService;
			Logger = logger;
		}


		public TEntity GetById(TKey id, bool includeDeleted = false, string includeProperties = "")
		{
			return QueryService.GetById(id, includeDeleted, includeProperties);
		}

		public void Update(TEntity entity, bool saveChanges = true, bool disposeUoW = true)
		{
			CUDService.Update(entity, saveChanges, disposeUoW);
		}

		public void Delete(TKey id, bool saveChanges = true, bool disposeUoW = true)
		{
			CUDService.Delete(id, saveChanges, disposeUoW);
		}

	}

	/// <summary>
	/// Defines the <see cref="Service{TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public abstract class Service<TEntity, TValidator> : Service<int, TEntity, TValidator>,
		IService<TEntity, TValidator>
		where TEntity : class, IBaseEntity
		where TValidator : IServiceValidator<TEntity>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Service{TEntity, TValidator}"/> class.
		/// </summary>
		/// <param name="validator">The validator<see cref="TValidator"/>.</param>
		/// <param name="repository">The repository<see cref="IRepository{TEntity}"/>.</param>
		/// <param name="logger">The logger<see cref="ICoreLogger"/>.</param>
		public Service(TValidator validator, IQueryService<int, TEntity, TValidator> queryService,
			ICUDService<int, TEntity, TValidator> cudService, ICoreLogger logger) :
			base(validator, queryService, cudService, logger)
		{
			{
			}
		}
	}
}
