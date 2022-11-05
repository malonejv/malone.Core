using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;
using malone.Core.Logging;
using malone.Core.Services.Requests;

namespace malone.Core.Services
{

	/// <summary>
	/// Defines the <see cref="BaseService{TEntity}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	public abstract class BaseService<TEntity> : IBaseService<TEntity>
		where TEntity : class
	{
		protected readonly IBaseRepository<TEntity> repository;
		protected readonly IUnitOfWork uow;
		protected readonly ICoreLogger logger;

		#region Constructor 

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseService{TEntity}"/> class.
		/// </summary>
		/// <param name="logger">The logger <see cref="ICoreLogger"/>.</param>
		/// <param name="repository">The repository <see cref="IBaseRepository{TEntity}"/>.</param>
		protected BaseService(IBaseRepository<TEntity> repository, IUnitOfWork uow, ICoreLogger logger)
		{
			this.repository = repository.ThrowIfNull();
			this.uow = uow.ThrowIfNull();
			this.logger = logger.ThrowIfNull();
		}


		#endregion

		///<inheritdoc/>
		public virtual IEnumerable<TEntity> GetAll(
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			bool includeDeleted = false,
			string includeProperties = ""
		)
		{
			try
			{
				var result = repository.GetAll(orderBy, includeDeleted, includeProperties);

				return result;
			}
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx =
					CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS400,
						typeof(TEntity));
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}

		///<inheritdoc/>
		public virtual IEnumerable<TEntity> Get<TFilter>(
			TFilter filter = default(TFilter),
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			bool includeDeleted = false,
			string includeProperties = "")
			where TFilter : class, IFilterExpression
		{
			try
			{
				var result = repository.Get(filter, orderBy, includeDeleted, includeProperties);

				return result;
			}
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx =
					CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS400,
						typeof(TEntity));
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}

		///<inheritdoc/>
		public TEntity GetEntity<TFilter>(
			TFilter filter = default(TFilter),
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			bool includeDeleted = false,
			string includeProperties = "")
			where TFilter : class, IFilterExpression
		{
			try
			{
				var result = repository.GetEntity(filter, orderBy, includeDeleted, includeProperties);

				return result;
			}
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx =
					CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS400,
						typeof(TEntity));
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}


		protected virtual ValidationResultList BeforeAddingValidation(TEntity entity)
			=> new ValidationResultList();

		///<inheritdoc/>
		public virtual void Add(TEntity entity, bool saveChanges = true)
		{
			try
			{
				entity.ThrowIfNull();

				var validationResult = BeforeAddingValidation(entity);
				if (!validationResult.IsValid)
				{
					throw new BusinessRulesValidationException(validationResult);
				}

				repository.Add(entity);

				if (saveChanges)
				{
					uow.SaveChanges();
				}
			}
			catch (BusinessRulesValidationException) { throw; }
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS401, typeof(TEntity));
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}

		protected virtual ValidationResultList BeforeUpdatingValidation(TEntity entity)
			=> new ValidationResultList();

		///<inheritdoc/>
		public virtual void Update(TEntity entity, bool saveChanges = true)
		{
			try
			{
				entity.ThrowIfNull();

				var validationResult = BeforeUpdatingValidation(entity);
				if (!validationResult.IsValid)
				{
					throw new BusinessRulesValidationException(validationResult);
				}

				repository.Update(entity);

				if (saveChanges)
				{
					uow.SaveChanges();
				}
			}
			catch (EntityNotFoundException ex)
			{
				if (logger != null)
				{
					logger.Warn(ex);
				}

				throw;
			}
			catch (BusinessRulesValidationException ex)
			{
				if (logger != null)
				{
					logger.Warn(ex);
				}

				throw;
			}
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS403, typeof(TEntity));
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}

		protected virtual ValidationResultList BeforeDeletingValidation(TEntity entity)
			=> new ValidationResultList();

		///<inheritdoc/>
		public virtual void Delete(TEntity entity, bool saveChanges = true)
		{
			try
			{
				entity.ThrowIfNull();

				var validationResult = BeforeDeletingValidation(entity);
				if (!validationResult.IsValid)
				{
					throw new BusinessRulesValidationException(validationResult);
				}

				if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
				{
					var softDelete = (ISoftDelete)entity;
					var fieldName = nameof(ISoftDelete.IsDeleted);
					var field = entity.GetType()
											  .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
											  .FirstOrDefault(f=>f.Name == fieldName || f.Name.Contains($"<{fieldName}>"));
					field.SetValue(entity, true);
					repository.Update(softDelete as TEntity);
				}
				else
				{
					repository.Delete(entity);
				}

				if (saveChanges)
				{
					uow.SaveChanges();
				}
			}
			catch (BusinessRulesValidationException) { throw; }
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS402, typeof(TEntity));
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}

	}

}
