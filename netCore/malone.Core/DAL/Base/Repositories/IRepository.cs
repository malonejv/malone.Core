using malone.Core.EL;
using malone.Core.EL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.DAL.Base.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        IEnumerable<TEntity> Get<TFilter>(
           TFilter filter = default(TFilter),
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false,
           string includeProperties = "")
            where TFilter : class, IFilter;

        IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = ""
           );

        TEntity GetById(
            object id,
            bool includeDeleted = false,
            string includeProperties = "");

        TEntity GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false,
            string includeProperties = "")
            where TFilter : class, IFilter;

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}
