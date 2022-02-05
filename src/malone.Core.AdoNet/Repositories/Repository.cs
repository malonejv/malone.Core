using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using malone.Core.AdoNet.Database;
using malone.Core.AdoNet.Entities;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Model;

namespace malone.Core.AdoNet.Repositories
{
    public abstract class Repository<TKey, TEntity> : BaseRepository<TEntity>, IRepository<TKey, TEntity>, IDisposable
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        public Repository(IContext context, ILogger logger) : base(context, logger) { }

        #region CRUD Operations

        #region GET BY ID

        protected abstract void ConfigureCommandForGetById(IDbCommand command, bool includeDeleted, string includeProperties);

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

        #endregion

        #region UPDATE

        protected virtual List<DbParameterWithValue> GetUpdateParameters(List<DbParameterWithValue> parameters, TEntity entity)
        {
            DbParameterWithValue parameter = typeof(TEntity).GetKeyParameter<TKey>(entity.Id);
            parameters.Add(parameter);

            parameters.AddRange(entity.GetNotKeyParameters<TKey, TEntity>().ToList());

            return parameters;
        }

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

        #endregion

        #region DELETE

        protected override List<DbParameterWithValue> GetDeleteParameters(List<DbParameterWithValue> parameters, TEntity entity)
        {
            DbParameterWithValue parameter = typeof(TEntity).GetKeyParameter<TKey>(entity.Id);
            parameters.Add(parameter);
            return parameters;
        }

        #endregion

        #endregion

    }


    public abstract class Repository<TEntity> : Repository<int, TEntity>, IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        public Repository(IContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
