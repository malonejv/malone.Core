using System;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.Entities.Model;
using malone.Core.Logging;

namespace malone.Core.Services
{
	public class CUDService<TKey, TEntity, TValidator> : BaseCUDService<TEntity, TValidator>, ICUDService<TKey, TEntity, TValidator>
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
		where TValidator : IServiceValidator<TKey, TEntity>
	{
		public ICUDRepository<TKey, TEntity> Repository { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Service{TKey, TEntity, TValidator}"/> class.
		/// </summary>
		/// <param name="validator">The validator<see cref="TValidator"/>.</param>
		/// <param name="repository">The repository<see cref="IRepository{TKey, TEntity}"/>.</param>
		/// <param name="logger">The logger<see cref="ICoreLogger"/>.</param>
		public CUDService(TValidator validator, ICUDRepository<TKey, TEntity> repository, ICoreLogger logger) : base(validator, repository, logger)
		{
			ServiceValidator = validator;
			Repository = repository;
			Logger = logger;
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
				entity.ThrowIfNull();
				entity.Id.ThrowIfNull();

				var uow = UnitOfWork.Create();

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
				id.ThrowIfNull();

				var uow = UnitOfWork.Create();


				TEntity entity = default;//this.GetById(id);
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

	}
}
