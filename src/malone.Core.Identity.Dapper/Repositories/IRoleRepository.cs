using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Model;
using System;

namespace malone.Core.Identity.Dapper.Repositories
{
    public interface IRoleRepository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
    }

    public interface IRoleRepository<TEntity> : IRoleRepository<int, TEntity>
        where TEntity : class, IBaseEntity<int>
    {
    }
}
