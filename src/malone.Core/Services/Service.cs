//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:39</date>

namespace malone.Core.Services
{
	using System;
	using System.Linq;
	using System.Reflection;
	using malone.Core.Commons.Exceptions;
	using malone.Core.Commons.Helpers.Extensions;
	using malone.Core.DataAccess.Repositories;
	using malone.Core.DataAccess.UnitOfWork;
	using malone.Core.Entities.Model;
	using malone.Core.Logging;
	using malone.Core.Services.Requests;

	/// <summary>
	/// Defines the <see cref="Service{TKey, TEntity}" />.
	/// </summary>
	/// <typeparam name="TKey">Type used for key property.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	public class Service<TKey, TEntity> : BaseService<TEntity>, IService<TKey, TEntity>
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
		/// <summary>
		/// The repository <see cref="IRepository{TKey, TEntity}"/>
		/// </summary>
		protected new IRepository<TKey, TEntity> repository;

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="Service{TKey, TEntity}"/> class.
		/// </summary>
		/// <param name="repository">The repository <see cref="IRepository{TKey, TEntity}"/>.</param>
		/// <param name="logger">The logger <see cref="ICoreLogger"/>.</param>
		public Service(IRepository<TKey, TEntity> repository, IUnitOfWork uow, ICoreLogger logger)
			: base(repository, uow, logger)
		{
			this.repository = repository.ThrowIfNull();
		}

		#endregion

		/// <inheritdoc />
		public virtual TEntity GetById(
			TKey id,
			bool includeDeleted = false,
			string includeProperties = "")
		{
			try
			{
				id.ThrowIfNull();

				var result = repository.GetById(id, includeDeleted, includeProperties);

				return result;
			}
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS400, typeof(TEntity));
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}

		///<inheritdoc/>
		public new virtual TKey Add(TEntity entity, bool saveChanges = true)
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

				return entity.Id;
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

		///<inheritdoc/>
		public virtual void Delete(TKey id, bool saveChanges = true)
		{
			try
			{
				id.ThrowIfNull();

				TEntity entity = this.GetById(id);
				if (entity == default(TEntity))
				{
					throw CoreExceptionFactory.CreateException<EntityNotFoundException>(CoreErrors.BUSVAL500, typeof(TEntity), id);
				}

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
											  .FirstOrDefault(f => f.Name == fieldName || f.Name.Contains($"<{fieldName}>"));
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


	///<inheritdoc />
	public class Service<TEntity> : Service<int, TEntity>,
		IService<TEntity>
		where TEntity : class, IBaseEntity
	{
		///<inheritdoc />
		public Service(ICoreLogger logger, IUnitOfWork uow, IRepository<TEntity> repository) :
			base( repository, uow, logger)
		{
		}
	}
}
