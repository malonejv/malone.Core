namespace malone.Core.AdoNet.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using malone.Core.AdoNet.Context;
	using malone.Core.AdoNet.Database;
	using malone.Core.AdoNet.Entities;
	using malone.Core.AdoNet.Entities.Filters;
	using malone.Core.Commons.Exceptions;
	using malone.Core.DataAccess.Context;
	using malone.Core.DataAccess.Repositories;
	using malone.Core.Entities.Filters;
	using malone.Core.Entities.Model;
	using malone.Core.Logging;

	/// <summary>
	/// Defines the <see cref="T: BaseRepository{TEntity}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable
		where TEntity : class
	{
		/// <summary>
		/// Gets the Context.
		/// </summary>
		protected CoreDbContext Context { get; private set; }

		/// <summary>
		/// Gets the Logger.
		/// </summary>
		protected ICoreLogger Logger { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T: BaseRepository{TEntity}"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="T: IContext"/>.</param>
		/// <param name="logger">The logger<see cref="T: ICoreLogger"/>.</param>
		public BaseRepository(IContext context, ICoreLogger logger)
		{
			CheckContext(context);
			CheckLogger(logger);

			Context = (CoreDbContext)context;
			Logger = logger;
		}

		/// <summary>
		/// The ConfigureCommandForGetAll.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="T: bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="T: string"/>.</param>
		protected abstract void ConfigureCommandForGetAll(IDbCommand command, bool includeDeleted, string includeProperties);

		/// <summary>
		/// The GetAll.
		/// </summary>
		/// <param name="orderBy">The orderBy<see cref="T: Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="T: bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="T: string"/>.</param>
		/// <returns>The <see cref="T: IEnumerable{TEntity}"/>.</returns>
		public virtual IEnumerable<TEntity> GetAll(
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			bool includeDeleted = false,
			string includeProperties = ""
		   )
		{
			ThrowIfDisposed();
			try
			{
				IQueryable<TEntity> queryableResult;

				var command = Context.CreateCommand();
				ConfigureCommandForGetAll(command, includeDeleted, includeProperties);
				queryableResult = GetQueryable(command, includeDeleted, orderBy);

				return queryableResult.ToList<TEntity>();
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
		/// The ConfigureCommandForGet.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="T: bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="T: string"/>.</param>
		protected abstract void ConfigureCommandForGet(IDbCommand command, bool includeDeleted, string includeProperties);

		/// <summary>
		/// The Get.
		/// </summary>
		/// <typeparam name="TFilter">.</typeparam>
		/// <param name="filter">The filter<see cref="T: TFilter"/>.</param>
		/// <param name="orderBy">The orderBy<see cref="T: Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="T: bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="T: string"/>.</param>
		/// <returns>The <see cref="T: IEnumerable{TEntity}"/>.</returns>
		public virtual IEnumerable<TEntity> Get<TFilter>(
		   TFilter filter = default(TFilter),
		   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
		   bool includeDeleted = false,
		   string includeProperties = "")
			where TFilter : class, IFilterExpression
		{
			ThrowIfDisposed();
			try
			{
				IQueryable<TEntity> queryableResult;

				var command = Context.CreateCommand();
				ConfigureCommandForGet(command, includeDeleted, includeProperties);

				IEnumerable<DbParameterWithValue> parameters;

				if (filter != default(TFilter) && typeof(IFilterExpressionAdoNet).IsAssignableFrom(typeof(TFilter)))
				{
					parameters = (filter as IFilterExpressionAdoNet).GetParameters(command);
					Context.AddCommandParameters(command, parameters);
				}

				queryableResult = GetQueryable(command, includeDeleted, orderBy);

				return queryableResult.ToList<TEntity>();
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
		/// The ConfigureCommandForGetEntity.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="T: bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="T: string"/>.</param>
		protected abstract void ConfigureCommandForGetEntity(IDbCommand command, bool includeDeleted, string includeProperties);

		/// <summary>
		/// The GetEntity.
		/// </summary>
		/// <typeparam name="TFilter">.</typeparam>
		/// <param name="filter">The filter<see cref="T: TFilter"/>.</param>
		/// <param name="orderBy">The orderBy<see cref="T: Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="T: bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="T: string"/>.</param>
		/// <returns>The <see cref="T: TEntity"/>.</returns>
		public virtual TEntity GetEntity<TFilter>(
			TFilter filter = default(TFilter),
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			bool includeDeleted = false,
			string includeProperties = "")
			where TFilter : class, IFilterExpression
		{
			ThrowIfDisposed();
			try
			{
				IQueryable<TEntity> queryableResult;

				var command = Context.CreateCommand();
				ConfigureCommandForGetEntity(command, includeDeleted, includeProperties);

				IEnumerable<DbParameterWithValue> parameters;

				if (filter != default(TFilter) && typeof(IFilterExpressionAdoNet).IsAssignableFrom(typeof(TFilter)))
				{
					parameters = (filter as IFilterExpressionAdoNet).GetParameters(command);
					Context.AddCommandParameters(command, parameters);
				}

				queryableResult = GetQueryable(command, includeDeleted, orderBy);

				return queryableResult.SingleOrDefault<TEntity>();
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
		/// The ConfigureCommandForInsert.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		protected abstract void ConfigureCommandForInsert(IDbCommand command);

		/// <summary>
		/// The GetInsertParameters.
		/// </summary>
		/// <param name="parameters">The parameters<see cref="T: List{DbParameterWithValue}"/>.</param>
		/// <param name="entity">The entity<see cref="T: TEntity"/>.</param>
		/// <returns>The <see cref="T: List{DbParameterWithValue}"/>.</returns>
		protected virtual List<DbParameterWithValue> GetInsertParameters(List<DbParameterWithValue> parameters, TEntity entity)
		{
			parameters = entity.GetParameters().ToList();
			return parameters;
		}

		/// <summary>
		/// The Insert.
		/// </summary>
		/// <param name="entity">The entity<see cref="T: TEntity"/>.</param>
		public virtual void Insert(TEntity entity)
		{
			ThrowIfDisposed();
			try
			{
				var command = Context.CreateCommand();
				ConfigureCommandForInsert(command);

				List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
				parameters = GetInsertParameters(parameters, entity);
				Context.AddCommandParameters(command, parameters);

				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS602, typeof(TEntity));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The ConfigureCommandForUpdate.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		protected abstract void ConfigureCommandForUpdate(IDbCommand command);

		/// <summary>
		/// The GetUpdateParameters.
		/// </summary>
		/// <param name="parameters">The parameters<see cref="T: List{DbParameterWithValue}"/>.</param>
		/// <param name="oldValues">The oldValues<see cref="T: TEntity"/>.</param>
		/// <param name="newValues">The newValues<see cref="T: TEntity"/>.</param>
		/// <returns>The <see cref="T: List{DbParameterWithValue}"/>.</returns>
		protected virtual List<DbParameterWithValue> GetUpdateParameters(List<DbParameterWithValue> parameters, TEntity oldValues, TEntity newValues)
		{
			parameters = newValues.GetParameters().ToList();
			return parameters;
		}

		/// <summary>
		/// The Update.
		/// </summary>
		/// <param name="oldValues">The oldValues<see cref="T: TEntity"/>.</param>
		/// <param name="newValues">The newValues<see cref="T: TEntity"/>.</param>
		public virtual void Update(TEntity oldValues, TEntity newValues)
		{
			ThrowIfDisposed();
			try
			{
				var command = Context.CreateCommand();
				ConfigureCommandForUpdate(command);

				List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
				parameters = GetUpdateParameters(parameters, oldValues, newValues);
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
		/// The ConfigureCommandForDelete.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		protected abstract void ConfigureCommandForDelete(IDbCommand command);

		/// <summary>
		/// The GetDeleteParameters.
		/// </summary>
		/// <param name="parameters">The parameters<see cref="T: List{DbParameterWithValue}"/>.</param>
		/// <param name="entity">The entity<see cref="T: TEntity"/>.</param>
		/// <returns>The <see cref="T: List{DbParameterWithValue}"/>.</returns>
		protected virtual List<DbParameterWithValue> GetDeleteParameters(List<DbParameterWithValue> parameters, TEntity entity)
		{
			parameters = entity.GetParameters().ToList();
			return parameters;
		}

		/// <summary>
		/// The Delete.
		/// </summary>
		/// <param name="entityToDelete">The entityToDelete<see cref="T: TEntity"/>.</param>
		public virtual void Delete(TEntity entityToDelete)
		{
			ThrowIfDisposed();
			try
			{
				var command = Context.CreateCommand();
				ConfigureCommandForDelete(command);

				List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
				parameters = GetDeleteParameters(parameters, entityToDelete);
				Context.AddCommandParameters(command, parameters);

				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS603, typeof(TEntity));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The CheckLogger.
		/// </summary>
		/// <param name="logger">The logger<see cref="T: ICoreLogger"/>.</param>
		private void CheckLogger(ICoreLogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException(nameof(logger));
			}
		}

		/// <summary>
		/// The CheckContext.
		/// </summary>
		/// <param name="context">The context<see cref="T: IContext"/>.</param>
		private void CheckContext(IContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			if (!(context is CoreDbContext))
			{
				//TODO: Implementar excepciones del core
				throw new ArgumentException();
			}
		}

		/// <summary>
		/// The GetQueryable.
		/// </summary>
		/// <param name="command">The command<see cref="T: IDbCommand"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="T: bool"/>.</param>
		/// <param name="orderBy">The orderBy<see cref="T: Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <returns>The <see cref="T: IQueryable{TEntity}"/>.</returns>
		protected IQueryable<TEntity> GetQueryable(
		   IDbCommand command,
		   bool includeDeleted = false,
		   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
		{
			try
			{

				if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
				{
					//TODO: Implementar solucion
					//Context.Db.AddParameterIsDeleted(command, includeDeleted);
				}

				IDataAdapter adapter = Context.CreateAdapter(command);
				DataSet ds = new DataSet();

				adapter.Fill(ds);

				command.Dispose();

				List<TEntity> result = new List<TEntity>();
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					TEntity entityMapped;
					foreach (DataRow row in ds.Tables[0].Rows)
					{
						entityMapped = Map(row);
						result.Add(entityMapped);
					}
				}

				var queryableResult = result.AsQueryable<TEntity>();
				if (orderBy != null && result.Count > 0)
				{
					return orderBy(queryableResult);
				}
				else
				{
					return queryableResult;
				}

			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The Map.
		/// </summary>
		/// <param name="row">The row<see cref="T: DataRow"/>.</param>
		/// <returns>The <see cref="T: TEntity"/>.</returns>
		protected abstract TEntity Map(DataRow row);

		/// <summary>
		/// Defines the _disposed.
		/// </summary>
		protected bool _disposed;

		/// <summary>
		/// The Dispose.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// The ThrowIfDisposed.
		/// </summary>
		protected void ThrowIfDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException(this.GetType().Name);
			}
		}

		/// <summary>
		/// The Dispose.
		/// </summary>
		/// <param name="disposing">The disposing<see cref="T: bool"/>.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && Context != null)
			{
				Context.Dispose();
			}
			_disposed = true;
			Context = null;
		}
	}
}
