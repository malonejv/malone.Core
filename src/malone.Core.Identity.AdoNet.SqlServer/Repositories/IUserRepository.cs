using System;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Model;

namespace malone.Core.Identity.AdoNet.SqlServer.Repositories
{
    public interface IUserRepository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
    }

    public interface IUserRepository<TEntity> : IUserRepository<int, TEntity>
        where TEntity : class, IBaseEntity<int>
    {
    }
}
