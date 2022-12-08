﻿using System;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Model;
using malone.Core.Logging;

namespace malone.Core.EF.Repositories
{
	public class CUDRepository<TKey, TEntity> : BaseCUDRepository<TEntity>, ICUDRepository<TKey, TEntity>, IDisposable
		where TKey : IEquatable<TKey>
		where TEntity : class, IBaseEntity<TKey>
	{
		#region Constructor

		public CUDRepository(IContext context, ICoreLogger logger) : base(context, logger) { }

		#endregion

		#region Public methods

		//public virtual void Update(TEntity entity)
		//{
		//  ThrowIfDisposed();
		//	try
		//	{
		//		var oldValues = GetById(entity.Id);

		//		if (oldValues.Equals(default(TEntity)))
		//		{
		//			throw CoreExceptionFactory.CreateException<TechnicalException>(CoreErrors.DATAACCESS601, typeof(TEntity));
		//		}

		//		Context.Entry(entity).State = EntityState.Modified;
		//	}
		//	catch (Exception ex)
		//	{
		//		var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS604, typeof(TEntity));
		//		if (Logger != null)
		//		{
		//			Logger.Error(techEx);
		//		}

		//		throw techEx;
		//	}
		//}

		#endregion
	}


	public class CUDRepository<TEntity> : CUDRepository<int, TEntity>, ICUDRepository<TEntity>
		where TEntity : class, IBaseEntity
	{
		public CUDRepository(IContext context, ICoreLogger logger) : base(context, logger)
		{
		}
	}
}