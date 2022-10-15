using System;
using System.Collections.Generic;
using System.Linq;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Filters;
using malone.Core.Logging;

namespace malone.Core.Services
{
	/// <summary>
	/// Defines the <see cref="BaseQueryService{TEntity}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	public class BaseQueryService<TEntity> : IBaseQueryService<TEntity>
		where TEntity : class
	{
		/// <summary>
		/// Gets or sets the QueryRepository.
		/// </summary>
		protected IBaseQueryRepository<TEntity> QueryRepository { get; private set; }

		/// <summary>
		/// Gets or sets the Logger.
		/// </summary>
		protected ICoreLogger Logger { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseService{TEntity, TValidator}"/> class.
		/// </summary>
		/// <param name="repository">The repository<see cref="IBaseRepository{TEntity}"/>.</param>
		/// <param name="logger">The logger<see cref="ICoreLogger"/>.</param>
		internal BaseQueryService(IBaseQueryRepository<TEntity> repository, ICoreLogger logger)
			:this(logger)
		{
			QueryRepository = repository.ThrowIfNull();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseService{TEntity, TValidator}"/> class.
		/// </summary>
		/// <param name="logger">The logger<see cref="ICoreLogger"/>.</param>
		internal BaseQueryService(ICoreLogger logger)
		{
			Logger = logger.ThrowIfNull();
		}

		/// <summary>
		/// The GetAll.
		/// </summary>
		/// <param name="orderBy">The orderBy<see cref="Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		/// <returns>The <see cref="IEnumerable{TEntity}"/>.</returns>
		public virtual IEnumerable<TEntity> GetAll(
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			bool includeDeleted = false,
			string includeProperties = ""
		)
		{
			try
			{
				var result = QueryRepository.GetAll(orderBy, includeDeleted, includeProperties);

				return result;
			}
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx =
					CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS400,
						typeof(TEntity));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The Get.
		/// </summary>
		/// <typeparam name="TFilter">.</typeparam>
		/// <param name="filter">The filter<see cref="TFilter"/>.</param>
		/// <param name="orderBy">The orderBy<see cref="Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		/// <returns>The <see cref="IEnumerable{TEntity}"/>.</returns>
		public virtual IEnumerable<TEntity> Get<TFilter>(
			TFilter filter = default(TFilter),
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			bool includeDeleted = false,
			string includeProperties = "")
			where TFilter : class, IFilterExpression
		{
			try
			{
				var result = QueryRepository.Get(filter, orderBy, includeDeleted, includeProperties);

				return result;
			}
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx =
					CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS400,
						typeof(TEntity));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The GetEntity.
		/// </summary>
		/// <typeparam name="TFilter">.</typeparam>
		/// <param name="filter">The filter<see cref="TFilter"/>.</param>
		/// <param name="orderBy">The orderBy<see cref="Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		/// <returns>The <see cref="TEntity"/>.</returns>
		public TEntity GetEntity<TFilter>(
			TFilter filter = default(TFilter),
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			bool includeDeleted = false,
			string includeProperties = "")
			where TFilter : class, IFilterExpression
		{
			try
			{
				var result = QueryRepository.GetEntity(filter, orderBy, includeDeleted, includeProperties);

				return result;
			}
			catch (TechnicalException) { throw; }
			catch (Exception ex)
			{
				var techEx =
					CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.BUSINESS400,
						typeof(TEntity));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}
	}
}
