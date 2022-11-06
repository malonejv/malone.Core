namespace malone.Core.AdoNet.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using malone.Core.AdoNet.Attributes;
	using malone.Core.AdoNet.Context;
	using malone.Core.AdoNet.Database;
	using malone.Core.AdoNet.Entities;
	using malone.Core.AdoNet.Entities.Filters;
	using malone.Core.Commons.Exceptions;
	using malone.Core.Commons.Helpers.Extensions;
	using malone.Core.DataAccess.Context;
	using malone.Core.DataAccess.Repositories;
	using malone.Core.Entities.Filters;
	using malone.Core.Entities.Model;
	using malone.Core.Logging;

	/// <summary>
	/// Defines the <see cref="BaseRepository{TEntity}" />.
	/// </summary>
	/// <typeparam name="TEntity">.</typeparam>
	public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable
		where TEntity : class, new()
	{
		protected CoreDbContext context;
		protected ICoreLogger logger;
		protected internal Type entityType;
		/// <summary>
		/// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="IContext"/>.</param>
		/// <param name="logger">The logger<see cref="ICoreLogger"/>.</param>
		public BaseRepository(IContext context, ICoreLogger logger)
		{
			CheckContext(context);
			CheckLogger(logger);

			this.context = context.ThrowIfNull().ThrowIfNotDeriveOfType<CoreDbContext>(nameof(context));
			this.logger = logger;
			entityType = this.GetType().GetInterface("IBaseRepository`1").GetGenericArguments()[0];
		}


		/// <summary>
		/// The ConfigureCommandForGetAll.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		protected virtual void ConfigureCommandForGetAll(IDbCommand command, bool includeDeleted) { }

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
			string includeProperties = "")
		{
			ThrowIfDisposed();
			try
			{
				IQueryable<TEntity> queryableResult;

				var command = context.CreateCommand();

				string tableName = entityType.GetTableName();
				var columns = entityType.GetColumnsInfo();

				string deletedCondition = DeleteCondintion(includeDeleted);

				string whereClause = !string.IsNullOrEmpty(deletedCondition) ? $" WHERE {deletedCondition}" : string.Empty;

				string columnNames = columns.Select(c => c.Name).Aggregate((i, j) => $"{i}, {j}");
				string query = $"SELECT {columnNames} FROM {tableName}{whereClause}";

				ConfigureCommandForGetAll(command, includeDeleted);
				queryableResult = GetQueryable(command, includeDeleted, orderBy);

				return queryableResult.ToList<TEntity>();
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The ConfigureCommandForGet.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		protected abstract void ConfigureCommandForGet(IDbCommand command, bool includeDeleted);

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
			ThrowIfDisposed();
			try
			{
				IQueryable<TEntity> queryableResult;

				var command = context.CreateCommand();

				string tableName = entityType.GetTableName();
				var columns = entityType.GetColumnsInfo();

				string deletedCondition = DeleteCondintion(includeDeleted);

				string whereClause = !string.IsNullOrEmpty(deletedCondition) ? $" WHERE {deletedCondition}" : string.Empty;

				IEnumerable<DbParameterWithValue> parameters;

				if (filter != default(TFilter) && typeof(IFilterExpressionAdoNet).IsAssignableFrom(typeof(TFilter)))
				{
					parameters = (filter as IFilterExpressionAdoNet).GetParameters(command);
					context.AddCommandParameters(command, parameters);
				}

				ConfigureCommandForGet(command, includeDeleted);

				queryableResult = GetQueryable(command, includeDeleted, orderBy);

				return queryableResult.ToList<TEntity>();
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The ConfigureCommandForGetEntity.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		protected abstract void ConfigureCommandForGetEntity(IDbCommand command, bool includeDeleted);

		/// <summary>
		/// The GetEntity.
		/// </summary>
		/// <typeparam name="TFilter">.</typeparam>
		/// <param name="filter">The filter<see cref="TFilter"/>.</param>
		/// <param name="orderBy">The orderBy<see cref="Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="includeProperties">The includeProperties<see cref="string"/>.</param>
		/// <returns>The <see cref="TEntity"/>.</returns>
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

				var command = context.CreateCommand();
				ConfigureCommandForGetEntity(command, includeDeleted);

				IEnumerable<DbParameterWithValue> parameters;

				if (filter != default(TFilter) && typeof(IFilterExpressionAdoNet).IsAssignableFrom(typeof(TFilter)))
				{
					parameters = (filter as IFilterExpressionAdoNet).GetParameters(command);
					context.AddCommandParameters(command, parameters);
				}

				queryableResult = GetQueryable(command, includeDeleted, orderBy);

				return queryableResult.SingleOrDefault<TEntity>();
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS600, typeof(TEntity).Name);
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The ConfigureCommandForInsert.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		protected abstract void ConfigureCommandForInsert(IDbCommand command);

		/// <summary>
		/// The GetInsertParameters.
		/// </summary>
		/// <param name="parameters">The parameters<see cref="List{DbParameterWithValue}"/>.</param>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		/// <returns>The <see cref="List{DbParameterWithValue}"/>.</returns>
		protected virtual List<DbParameterWithValue> GetInsertParameters(List<DbParameterWithValue> parameters, TEntity entity)
		{
			parameters = entity.GetParameters().ToList();
			return parameters;
		}

		/// <summary>
		/// The Insert.
		/// </summary>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		public virtual void Add(TEntity entity)
		{
			ThrowIfDisposed();
			try
			{
				var command = context.CreateCommand();
				ConfigureCommandForInsert(command);

				List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
				parameters = GetInsertParameters(parameters, entity);
				context.AddCommandParameters(command, parameters);

				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS602, typeof(TEntity));
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The ConfigureCommandForUpdate.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		protected abstract void ConfigureCommandForUpdate(IDbCommand command);

		/// <summary>
		/// The GetUpdateParameters.
		/// </summary>
		/// <param name="parameters">The parameters<see cref="List{DbParameterWithValue}"/>.</param>
		/// <param name="entity">The oldValues<see cref="TEntity"/>.</param>
		/// <returns>The <see cref="List{DbParameterWithValue}"/>.</returns>
		protected virtual List<DbParameterWithValue> GetUpdateParameters(List<DbParameterWithValue> parameters, TEntity entity)
		{
			parameters = entity.GetParameters().ToList();
			return parameters;
		}

		/// <summary>
		/// The Update.
		/// </summary>
		/// <param name="entity">The oldValues<see cref="TEntity"/>.</param>
		public virtual void Update(TEntity entity)
		{
			ThrowIfDisposed();
			try
			{
				var command = context.CreateCommand();
				ConfigureCommandForUpdate(command);

				List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
				parameters = GetUpdateParameters(parameters, entity);
				context.AddCommandParameters(command, parameters);

				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS604, typeof(TEntity));
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The ConfigureCommandForDelete.
		/// </summary>
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		protected abstract void ConfigureCommandForDelete(IDbCommand command);

		/// <summary>
		/// The GetDeleteParameters.
		/// </summary>
		/// <param name="parameters">The parameters<see cref="List{DbParameterWithValue}"/>.</param>
		/// <param name="entity">The entity<see cref="TEntity"/>.</param>
		/// <returns>The <see cref="List{DbParameterWithValue}"/>.</returns>
		protected virtual List<DbParameterWithValue> GetDeleteParameters(List<DbParameterWithValue> parameters, TEntity entity)
		{
			parameters = entity.GetParameters().ToList();
			return parameters;
		}

		/// <summary>
		/// The Delete.
		/// </summary>
		/// <param name="entityToDelete">The entityToDelete<see cref="TEntity"/>.</param>
		public virtual void Delete(TEntity entityToDelete)
		{
			ThrowIfDisposed();
			try
			{
				var command = context.CreateCommand();
				ConfigureCommandForDelete(command);

				List<DbParameterWithValue> parameters = new List<DbParameterWithValue>();
				parameters = GetDeleteParameters(parameters, entityToDelete);
				context.AddCommandParameters(command, parameters);

				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS603, typeof(TEntity));
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}

		/// <summary>
		/// The CheckLogger.
		/// </summary>
		/// <param name="logger">The logger<see cref="ICoreLogger"/>.</param>
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
		/// <param name="context">The context<see cref="IContext"/>.</param>
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
		/// <param name="command">The command<see cref="IDbCommand"/>.</param>
		/// <param name="includeDeleted">The includeDeleted<see cref="bool"/>.</param>
		/// <param name="orderBy">The orderBy<see cref="Func{IQueryable{TEntity}, IOrderedQueryable{TEntity}}"/>.</param>
		/// <returns>The <see cref="IQueryable{TEntity}"/>.</returns>
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

				IDataAdapter adapter = context.CreateAdapter(command);
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
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}

		protected bool ConfigureParameterForSoftDelete(Type entityType, List<string> columns, bool includeDeleted)
		{
			var softDeleted = false;
			if (includeDeleted)
			{
				var allowSoftDelete = typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity));
				if (allowSoftDelete)
				{
					var columnName = entityType.GetColumnName(nameof(ISoftDelete.IsDeleted));
					if (!columns.Contains(columnName))
					{
						columns.Add(columnName);
					}
					softDeleted = true;
				}
			}
			return softDeleted;
		}

		protected string ConfigureWhereClause(IEnumerable<ColumnAttribute> columns)
		{
			StringBuilder whereClause = new StringBuilder(" WHERE ");
			foreach (var column in columns)
			{
				whereClause.Append($" {column} = ");
			}
			return whereClause.ToString();
		}

		/// <summary>
		/// The Map.
		/// </summary>
		/// <param name="row">The row<see cref="DataRow"/>.</param>
		/// <returns>The <see cref="TEntity"/>.</returns>
		protected virtual TEntity Map(DataRow row)
		{
			TEntity entity = null;
			if (row != null)
			{
				entity = new TEntity();
				var tEntity = entityType.GetType();

				var columns = entityType.GetColumnsInfo();
				foreach (var column in columns)
				{
					tEntity.GetProperty(column.PropertyName).SetValue(entityType, Convert.ChangeType(row[column.Name], column.PropertyType));
				}
			}
			return entity;
		}

		private string DeleteCondintion(bool includeDeleted)
		{
			if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
			{
				if (!includeDeleted)
				{
					var isDeletedColumn = entityType.GetColumnInfo(nameof(ISoftDelete.IsDeleted));
					return $"{isDeletedColumn.Name} = 0";
				}
			}
			return string.Empty;
		}

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
		/// <param name="disposing">The disposing<see cref="bool"/>.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && context != null)
			{
				context.Dispose();
			}
			_disposed = true;
			context = null;
		}
	}
}
