using malone.Core.DAL.Base.Repositories;
using malone.Core.EL;
using malone.Core.EL.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.DAL.AdoNet.Repositories
{
    public interface IAdoNetRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        IEnumerable<TEntity> Get<TFilter>(
           TFilter filter = default(TFilter),
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false)
            where TFilter : class, IFilterExpressionAdoNet;

        IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false
           );

        TEntity GetById(
            object id,
            bool includeDeleted = false);

        TEntity GetEntity<TFilter>(
            TFilter filter = default(TFilter),
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool includeDeleted = false)
            where TFilter : class, IFilterExpressionAdoNet;

    }
}
