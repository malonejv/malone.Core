using System;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.Entities.Model;
using malone.Core.Logging;

namespace malone.Core.Services
{
	/// <summary>
	/// Defines the <see cref="BaseCUDService{TEntity, TValidator}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	/// <typeparam name="TValidator">.</typeparam>
	public abstract class BaseCUDService<TEntity, TValidator> : IBaseCUDService<TEntity, TValidator>
		where TEntity : class
		where TValidator : IBaseServiceValidator<TEntity>
	{
		/// <summary>
		/// Gets or sets the CUDRepository.
		/// </summary>
		public IBaseCUDRepository<TEntity> CUDRepository { get; set; }

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
		/// <param name="validator">The validator <see cref="TValidator"/>.</param>
		/// <param name="repository">The repository <see cref="IBaseRepository{TEntity}"/>.</param>
		/// <param name="logger">The logger <see cref="ICoreLogger"/>.</param>
		protected BaseCUDService(TValidator validator, IBaseCUDRepository<TEntity> repository, ICoreLogger logger)
		{
			ServiceValidator = validator.ThrowIfNull();
			CUDRepository = repository.ThrowIfNull();
			Logger = logger.ThrowIfNull();
		}

		/// <summary>
		/// The Add.
		/// </summary>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		/// <param name="saveChanges">The saveChanges<see cref="bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW<see cref="bool"/>.</param>
		public virtual void Add(TEntity entity, bool saveChanges = true, bool disposeUoW = true)
		{
			try
			{
				entity.ThrowIfNull();

				var uow = UnitOfWork.Create();

				var validationResult = ServiceValidator.Validate(ServiceValidator.ExecuteAddValidationRules, ServiceValidator.AddValidationRules);
				if (!validationResult.IsValid)
				{
					throw new BusinessRulesValidationException(validationResult);
				}

				CUDRepository.Insert(entity);

				if (saveChanges)
				{
					uow.SaveChanges();
				}

				if (disposeUoW)
				{
					uow.Dispose();
				}
			}
			catch (BusinessRulesValidationException) { throw; }
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS401, typeof(TEntity));
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
		/// <param name="oldValues">The oldValues<see cref="TEntity"/>.</param>
		/// <param name="newValues">The newValues<see cref="TEntity"/>.</param>
		/// <param name="saveChanges">The saveChanges<see cref="bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW<see cref="bool"/>.</param>
		public virtual void Update(TEntity oldValues, TEntity newValues, bool saveChanges = true, bool disposeUoW = true)
		{
			try
			{
				oldValues.ThrowIfNull();
				newValues.ThrowIfNull();

				var uow = UnitOfWork.Create();

				var validationResult = ServiceValidator.Validate(ServiceValidator.ExecuteUpdateValidationRules, ServiceValidator.UpdateValidationRules);
				if (!validationResult.IsValid)
				{
					throw new BusinessRulesValidationException(validationResult);
				}

				CUDRepository.Update(oldValues, newValues);
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
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		/// <param name="saveChanges">The saveChanges<see cref="bool"/>.</param>
		/// <param name="disposeUoW">The disposeUoW<see cref="bool"/>.</param>
		public virtual void Delete(TEntity entity, bool saveChanges = true, bool disposeUoW = true)
		{
			try
			{
				entity.ThrowIfNull();

				var uow = UnitOfWork.Create();

				var validationResult = ServiceValidator.Validate(ServiceValidator.ExecuteDeleteValidationRules, ServiceValidator.DeleteValidationRules);
				if (!validationResult.IsValid)
				{
					throw new BusinessRulesValidationException(validationResult);
				}

				if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
				{
					var softDelete = (ISoftDelete)entity;
					softDelete.IsDeleted = true;
					CUDRepository.Update(entity, softDelete as TEntity);
				}
				else
				{
					CUDRepository.Delete(entity);
				}

				if (saveChanges)
				{
					uow.SaveChanges();
				}

				if (disposeUoW)
				{
					uow.Dispose();
				}
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
