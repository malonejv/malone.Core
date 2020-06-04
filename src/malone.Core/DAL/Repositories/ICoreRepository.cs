using malone.Core.EL.Filters;
using malone.Core.EL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.DAL.Repositories
{
    public interface ICoreRepository<TKey, TEntity, TErrorCoder>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
        where TErrorCoder : Enum
    {

        IEnumerable<TEntity> Get<TFilter>(
           TFilter filter = default(TFilter),
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false,
           string includeProperties = "")
            where TFilter : class, IFilterExpression;

        IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = ""
           );

        TEntity GetById(
            TKey id,
            bool includeDeleted = false,
            string includeProperties = "");

        TEntity GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = "")
            where TFilter : class, IFilterExpression;


        void Insert(TEntity entity);

        void Delete(TKey id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }

    public interface ICoreRepository<TEntity, TErrorCoder> : ICoreRepository<int, TEntity, TErrorCoder>
        where TEntity : class, IBaseEntity
        where TErrorCoder : Enum
    {
    }
}
