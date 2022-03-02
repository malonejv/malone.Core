//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:39</date>

namespace malone.Core.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using malone.Core.Commons.Exceptions;
	using malone.Core.DataAccess.Repositories;
	using malone.Core.DataAccess.UnitOfWork;
	using malone.Core.Entities.Filters;
	using malone.Core.Entities.Model;
	using malone.Core.Logging;

	/// <summary>
	/// Defines the <see cref="Service{TKey, TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public abstract class Service<TKey, TEntity, TValidator> : BaseService<TEntity, TValidator>, IService<TKey, TEntity, TValidator>
where TKey : IEquatable<TKey>
where TEntity : class, IBaseEntity<TKey>
where TValidator : IServiceValidator<TKey, TEntity>
	{
		/// <summary>
		/// Gets or sets the Repository.
		/// </summary>
		public IRepository<TKey, TEntity> Repository { get; set; }

		/// <summary>
		/// Gets or sets the ServiceValidator.
		/// </summary>
		public TValidator ServiceValidator { get; set; }

		/// <summary>
		/// Gets or sets the Logger.
		/// </summary>
		public ICoreLogger Logger { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Service{TKey, TEntity, TValidator}"/> class.
		/// </summary>
		/// <param name="businessValidator">The businessValidator<see cref="TValidator"/>.</param>
		/// <param name="repository">The repository<see cref="IRepository{TKey, TEntity}"/>.</param>
		/// <param name="logger">The logger<see cref="ICoreLogger"/>.</param>
		public Service(TValidator businessValidator, IRepository<TKey, TEntity> repository, ICoreLogger logger) : base(businessValidator, repository, logger)
		{
			ServiceValidator = businessValidator;
			Repository = repository;
			Logger = logger;
		}

		/// <summary>
		/// The GetById.
		/// </summary>
		/// <param name="id">The id<see cref="TKey"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		/// <returns>The <see cref="TEntity"/>.</returns>
		public virtual TEntity GetById(
TKey id,
bool includeDeleted = false,
string includeProperties = "")
		{
			try
			{
				CheckId(id);

				var result = Repository.GetById(id, includeDeleted, includeProperties);

				return result;
			}
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS400, typeof(TEntity));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The Update.
		/// </summary>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		/// <param name="saveChanges">The saveChanges<see cref="bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW<see cref="bool"/>.</param>
		public virtual void Update(TEntity entity, bool saveChanges = true, bool disposeUoW = true)
		{
			try
			{
				var uow = UnitOfWork.Create();
				CheckEntity(entity);
				CheckEntityId(entity);

				var validationResult = ServiceValidator.Validate(ServiceValidator.ExecuteUpdateValidationRules, ServiceValidator.UpdateValidationRules);
				if (!validationResult.IsValid)
				{
					throw new BusinessRulesValidationException(validationResult);
				}
				//TODO: Revisar
				Repository.Update(entity, null);
				if (saveChanges)
				{
					uow.SaveChanges();
				}

				if (disposeUoW)
				{
					uow.Dispose();
				}
			}
			catch (EntityNotFoundException ex)
			{
				if (Logger != null)
				{
					Logger.Warn(ex);
				}

				throw;
			}
			catch (BusinessRulesValidationException ex)
			{
				if (Logger != null)
				{
					Logger.Warn(ex);
				}

				throw;
			}
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS403, typeof(TEntity));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The Delete.
		/// </summary>
		/// <param name="id">The id<see cref="TKey"/>.</param>
		/// <param name="saveChanges">The saveChanges<see cref="bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW<see cref="bool"/>.</param>
		public virtual void Delete(TKey id, bool saveChanges = true, bool disposeUoW = true)
		{
			try
			{
				var uow = UnitOfWork.Create();
				CheckId(id);

				var entity = this.GetById(id);
				if (entity == default(TEntity))
				{
					throw CoreExceptionFactory.CreateException<EntityNotFoundException>(CoreErrors.BUSVAL500, typeof(TEntity), id);
				}

				Delete(entity, saveChanges, disposeUoW);
			}
			catch (BusinessRulesValidationException) { throw; }
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS402, typeof(TEntity));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The CheckEntity.
		/// </summary>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		protected void CheckEntity(TEntity entity)
		{
			if (entity == default(TEntity))
			{
				throw new ArgumentException(nameof(entity));
			}
		}

		/// <summary>
		/// The CheckEntityId.
		/// </summary>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		protected override void CheckEntityId(TEntity entity)
		{
			if (entity.Id.Equals(default(TKey)))
			{
				throw new ArgumentException(nameof(entity.Id));
			}
		}

		/// <summary>
		/// The CheckId.
		/// </summary>
		/// <param name="id">The id<see cref="TKey"/>.</param>
		protected void CheckId(TKey id)
		{
			if (id.Equals(default(TKey)))
			{
				throw new ArgumentException(nameof(id));
			}
		}

		/// <summary>
		/// The CheckId.
		/// </summary>
		/// <param name="args">The args<see cref="object[]"/>.</param>
		protected override void CheckId(params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException(nameof(args));
			}

			if (args.Length == 0)
			{
				throw new ArgumentException(nameof(args));
			}

			int id = (int)args[0];

			if (id.Equals(default(TKey)))
			{
				throw new ArgumentException(nameof(id));
			}
		}
	}

	/// <summary>
	/// Defines the <see cref="Service{TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public abstract class Service<TEntity, TValidator> : Service<int, TEntity, TValidator>, IService<TEntity, TValidator>
where TEntity : class, IBaseEntity
where TValidator : IServiceValidator<TEntity>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Service{TEntity, TValidator}"/> class.
		/// </summary>
		/// <param name="businessValidator">The businessValidator<see cref="TValidator"/>.</param>
		/// <param name="repository">The repository<see cref="IRepository{TEntity}"/>.</param>
		/// <param name="logger">The logger<see cref="ICoreLogger"/>.</param>
		public Service(TValidator businessValidator, IRepository<TEntity> repository, ICoreLogger logger)
: base(businessValidator, repository, logger)
		{
		}
	}
}
