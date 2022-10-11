using System;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Model;
using malone.Core.Logging;

namespace malone.Core.Services
{
	public class QueryService<TKey, TEntity, TValidator> : BaseQueryService<TEntity, TValidator>, IQueryService<TKey, TEntity, TValidator>
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
		where TValidator : IServiceValidator<TKey, TEntity>
	{
		public IQueryRepository<TKey, TEntity> Repository { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Service{TKey, TEntity, TServiceValidator}"/> class.
		/// </summary>
		/// <param name="validator">The validator<see cref="TValidator"/>.</param>
		/// <param name="repository">The repository<see cref="IRepository{TEntity}"/>.</param>
		/// <param name="logger">The logger<see cref="ICoreLogger"/>.</param>
		public QueryService(TValidator validator, IQueryRepository<TKey, TEntity> repository, ICoreLogger logger) : base(repository, logger)
		{
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
				id.ThrowIfNull();

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
	}
}
