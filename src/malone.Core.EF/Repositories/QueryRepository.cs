using System;
using System.Linq;
using malone.Core.Commons.Exceptions;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Model;
using malone.Core.Logging;

namespace malone.Core.EF.Repositories
{
	public class QueryRepository<TKey, TEntity> : BaseQueryRepository<TEntity>, IQueryRepository<TKey, TEntity>, IDisposable
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
		#region Constructor

		public QueryRepository(IContext context, ICoreLogger logger) : base(context, logger) { }

		#endregion

		#region Public methods

		public virtual TEntity GetById(
			TKey id,
			bool includeDeleted = false,
			string includeProperties = "")
		{
			ThrowIfDisposed();
			try
			{
				IQueryable<TEntity> query = EntityDbSet;

				query = Get(query, null, includeDeleted, includeProperties);

				return query.FirstOrDefault(e => e.Id.Equals(id));
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS601, typeof(TEntity));
				if (Logger != null)
				{
					Logger.Error(techEx);
				}

				throw techEx;
			}
		}

		#endregion

	}

	public class QueryRepository<TEntity> : QueryRepository<int, TEntity>, IQueryRepository<TEntity>, IDisposable
		where TEntity : class, IBaseEntity
	{
		public QueryRepository(IContext context, ICoreLogger logger) : base(context, logger)
		{
		}
	}
}
