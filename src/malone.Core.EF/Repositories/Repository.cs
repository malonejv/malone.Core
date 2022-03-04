﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using malone.Core.Commons.Exceptions;
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
		public new IQueryRepository<TKey, TEntity> QueryRepository { get; }
		public new IDataManipulationRepository<TKey, TEntity> DataManipulationRepository { get; }

		#region Constructor

		public Repository(IContext context, ICoreLogger logger, IQueryRepository<TKey, TEntity> QueryRepository, IDataManipulationRepository<TKey, TEntity> DataManipulationRepository) :
			base(context, logger, QueryRepository, DataManipulationRepository)
		{ }

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
		public Repository(IContext context, ICoreLogger logger, IQueryRepository<int, TEntity> QueryRepository, IDataManipulationRepository<int, TEntity> DataManipulationRepository) :
			base(context, logger, QueryRepository, DataManipulationRepository)
		{
		}
	}

}

