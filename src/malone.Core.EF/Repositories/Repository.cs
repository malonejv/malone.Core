using System;
using System.Linq;
using malone.Core.Commons.Exceptions;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;
using malone.Core.Logging;

namespace malone.Core.EF.Repositories.Implementations
{
	public class Repository<TKey, TEntity> : BaseRepository<TEntity>, IRepository<TKey, TEntity>, IDisposable
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{

		#region Constructor 

		public Repository(IContext context, ICoreLogger logger) : base(context, logger) { }

		#endregion

		#region Public methods

		public virtual TEntity GetById(
			TKey id,
			bool includeDeleted = false)
		{
			ThrowIfDisposed();
			try
			{
				IQueryable<TEntity> query = entityDbSet;

				query = Get(query, null, includeDeleted, includeProperties);

				return query.FirstOrDefault(e => e.Id.Equals(id));
			}
			catch (Exception ex)
			{
				var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS601, typeof(TEntity));
				if (logger != null)
				{
					logger.Error(techEx);
				}

				throw techEx;
			}
		}

		#endregion

	}


	public class Repository<TEntity> :
		Repository<int, TEntity>,
		IRepository<TEntity>
		where TEntity : class, IBaseEntity
	{
		public Repository(IContext context, ICoreLogger logger) :
			base(context, logger)
		{
		}
	}

}

