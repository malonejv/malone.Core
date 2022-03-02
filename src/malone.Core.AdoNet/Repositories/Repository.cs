namespace malone.Core.AdoNet.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using malone.Core.AdoNet.Database;
	using malone.Core.AdoNet.Entities;
	using malone.Core.Commons.Exceptions;
	using malone.Core.DataAccess.Context;
	using malone.Core.DataAccess.Repositories;
	using malone.Core.Entities.Model;
	using malone.Core.Logging;

	/// <summary>
	/// Defines the <see cref="Repository{TKey, TEntity}" />.
	/// </summary>
	/// <typeparam name="TKey">.</typeparam>
	/// <typeparam name="TEntity">.</typeparam>
	public abstract class Repository<TKey, TEntity> : BaseRepository<TEntity>, IRepository<TKey, TEntity>, IDisposable
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Repository{TKey, TEntity}"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="IContext"/>.</param>
		/// <param name="logger">The logger<see cref="ICoreLogger"/>.</param>
		public Repository(IContext context, ICoreLogger logger) : base(context, logger)
		{
		}

		/// <summary>
		/// The ConfigureCommandForGetById.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		protected abstract void ConfigureCommandForGetById(IDbCommand command, bool includeDeleted, string includeProperties);

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
			ThrowIfDisposed();
			try
			{
				IQueryable<TEntity> queryableResult;

				var command = Context.CreateCommand();
				ConfigureCommandForGetById(command, includeDeleted, includeProperties);

				List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
				DbParameterWithValue parameter = typeof(TEntity).GetKeyParameter<TKey>(id);
				parameters.Add(parameter);
				Context.AddCommandParameters(command, parameters);

				queryableResult = GetQueryable(command, includeDeleted);

				return queryableResult.FirstOrDefault();
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The GetUpdateParameters.
		/// </summary>
		/// <param name="parameters">The parameters<see cref="List{DbParameterWithValue}"/>.</param>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		/// <returns>The <see cref="List{DbParameterWithValue}"/>.</returns>
		protected virtual List<DbParameterWithValue> GetUpdateParameters(List<DbParameterWithValue> parameters, TEntity entity)
		{
			DbParameterWithValue parameter = typeof(TEntity).GetKeyParameter<TKey>(entity.Id);
			parameters.Add(parameter);

			parameters.AddRange(entity.GetNotKeyParameters<TKey, TEntity>().ToList());

			return parameters;
		}

		/// <summary>
		/// The Update.
		/// </summary>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		public virtual void Update(TEntity entity)
		{
			ThrowIfDisposed();
			try
			{
				var command = Context.CreateCommand();
				ConfigureCommandForUpdate(command);

				List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
				parameters = GetUpdateParameters(parameters, entity);
				Context.AddCommandParameters(command, parameters);

				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS604, typeof(TEntity));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The GetDeleteParameters.
		/// </summary>
		/// <param name="parameters">The parameters<see cref="List{DbParameterWithValue}"/>.</param>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		/// <returns>The <see cref="List{DbParameterWithValue}"/>.</returns>
		protected override List<DbParameterWithValue> GetDeleteParameters(List<DbParameterWithValue> parameters, TEntity entity)
		{
			DbParameterWithValue parameter = typeof(TEntity).GetKeyParameter<TKey>(entity.Id);
			parameters.Add(parameter);
			return parameters;
		}
	}

	/// <summary>
	/// Defines the <see cref="Repository{TEntity}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	public abstract class Repository<TEntity> : Repository<int, TEntity>, IRepository<TEntity>
		where TEntity : class, IBaseEntity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="IContext"/>.</param>
		/// <param name="logger">The logger<see cref="ICoreLogger"/>.</param>
		public Repository(IContext context, ICoreLogger logger) : base(context, logger)
		{
		}
	}
}
