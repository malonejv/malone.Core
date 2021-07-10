using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace malone.Core.Identity.Dapper.Repositories
{
    public interface IRoleRepository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        IEnumerable<TEntity> GetWhereIdIn(
           TKey[] ids,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           bool includeDeleted = false,
           string includeProperties = "");


    }

    public interface IRoleRepository<TEntity> : IRoleRepository<int, TEntity>
        where TEntity : class, IBaseEntity<int>
    {
    }
}
