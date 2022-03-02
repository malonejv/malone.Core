using System;
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
        #region Constructor

        public Repository(IContext context, ICoreLogger logger) : base(context, logger) { }

        #endregion

        #region CRUD Operations

        #region UPDATE

        public virtual void Update(TEntity entity)
        {
            try
            {
                var oldValues = GetById(entity.Id);

                if (oldValues.Equals(default(TEntity)))
                {
                    throw CoreExceptionFactory.CreateException<TechnicalException>(CoreErrors.DATAACCESS601, typeof(TEntity));
                }

                Context.Entry(entity).State = EntityState.Modified;
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

        //public virtual void Update(TEntity oldValues, TEntity newValues)
        //{
        //    try
        //    {
        //        _context.Entry(oldValues).State = EntityState.Detached;

        //        _context.Entry(newValues).State = EntityState.Modified;
        //    }
        //    catch (Exception ex)
        //    {
        //        var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.DATAACCESS604, typeof(TEntity));
        //        if (Logger != null) Logger.Error(techEx);

        //        throw techEx;
        //    }
        //}

        #endregion

        #region Private And Protected Methods

        public void SetAddOrUpdate<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : IBaseEntity
        {
            foreach (var entity in entities)
            {
                Context.Entry(entity).State = entity.AddOrUpdate();
            }
        }

        #endregion

        #endregion

    }


    public class Repository<TEntity> :
        Repository<int, TEntity>,
        IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        public Repository(IContext context, ICoreLogger logger) : base(context, logger)
        {
        }
    }

}

