using System;
using System.Collections.Generic;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.EF.Entities;
using malone.Core.Entities.Model;
using malone.Core.Logging;

namespace malone.Core.EF.Repositories.Implementations
{
	public class Repository<TKey, TEntity> : BaseRepository<TEntity>, IRepository<TKey, TEntity>, IDisposable
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
		protected new IQueryRepository<TKey, TEntity> QueryRepository { get; private set; }
		protected new ICUDRepository<TKey, TEntity> CUDRepository { get; private set; }

		#region Constructor & Initializer

		public Repository(IContext context, ICoreLogger logger) : base(context, logger) { }

		protected override void InitializeRepositories(IContext context, ICoreLogger logger)
		{
			QueryRepository = new QueryRepository<TKey, TEntity>(context, logger);
			CUDRepository = new CUDRepository<TKey, TEntity>(context, logger);
		}

		#endregion

		#region Public methods

		public TEntity GetById(TKey id, bool includeDeleted = false, string includeProperties = "")
		{
			return QueryRepository.GetById(id, includeDeleted, includeProperties);
		}

		public void SetAddOrUpdate<T>(IEnumerable<T> entities)
			where T : IBaseEntity
		{
			foreach (var entity in entities)
			{
				Context.Entry(entity).State = entity.AddOrUpdate();
			}
		}

		#endregion

	}


	public class Repository<TEntity> :
		Repository<int, TEntity>,
		IRepository<TEntity>
		where TEntity : class, IBaseEntity
	{
		public Repository(IContext context, ICoreLogger logger, IQueryRepository<int, TEntity> queryRepository, ICUDRepository<int, TEntity> cudRepository) :
			base(context, logger)
		{
		}
	}

}

