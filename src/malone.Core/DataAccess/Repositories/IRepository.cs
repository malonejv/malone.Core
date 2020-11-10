using malone.Core.Entities.Filters;
using malone.Core.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.DataAccess.Repositories
{
    public interface IRepository<TKey, TEntity> : IBaseRepository<TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {

        //IEnumerable<TEntity> Get<TFilter>(
        //   TFilter filter = default(TFilter),
        //   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //   bool includeDeleted = false,
        //   string includeProperties = "")
        //    where TFilter : class, IFilterExpression;

        //IEnumerable<TEntity> GetAll(
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    bool includeDeleted = false,
        //    string includeProperties = ""
        //   );

        TEntity GetById(
            TKey id,
            bool includeDeleted = false,
            string includeProperties = "");

        //TEntity GetEntity<TFilter>(
        //    TFilter filter = default(TFilter),
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    bool includeDeleted = false,
        //    string includeProperties = "")
        //    where TFilter : class, IFilterExpression;


        //void Insert(TEntity entity);

        //void Delete(TEntity entity);

        //void Update(TEntity oldValues, TEntity newValues);

        void Update(TEntity entity);

    }

    public interface IRepository<TEntity> : IRepository<int, TEntity>
        where TEntity : class, IBaseEntity
    {
    }
}
