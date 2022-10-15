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
		protected new IQueryService<TKey, TEntity> QueryService { get; }
		protected new ICUDService<TKey, TEntity, TValidator> CUDService { get; }

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="Service{TKey, TEntity, TValidator}"/> class.
		/// </summary>
		/// <param name="validator">The validator <typeparamref name="TValidator"/>.</param>
		/// <param name="queryRepository">The repository <see cref="IRepository{TKey, TEntity}"/>.</param>
		/// <param name="cudRepository">The repository <see cref="IRepository{TKey, TEntity}"/>.</param>
		/// <param name="logger">The logger <see cref="ICoreLogger"/>.</param>
		public Service(TValidator validator, ICoreLogger logger, IQueryRepository<TKey, TEntity> queryRepository, ICUDRepository<TKey, TEntity> cudRepository) :
			base(validator, logger)
		{
			QueryService = new QueryService<TKey, TEntity>(queryRepository, logger);
			CUDService = new CUDService<TKey, TEntity, TValidator>(validator, cudRepository, logger);
		}

		#endregion

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


	///<inheritdoc />
	public class Service<TEntity, TValidator> : Service<int, TEntity, TValidator>,
		IService<TEntity, TValidator>
		where TEntity : class, IBaseEntity
		where TValidator : IServiceValidator<TEntity>
	{
		///<inheritdoc />
		public Service(TValidator validator, ICoreLogger logger, IQueryRepository<TEntity> queryRepository, ICUDRepository<int, TEntity> cudRepository) :
			base(validator, logger, queryRepository, cudRepository )
		{
		}
	}
}
