using System;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Model;

namespace malone.Core.Identity.Dapper.Repositories
{
    public interface IRoleRepository<TKey, TEntity> : ICustomRepository<TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        int Delete(TKey roleId);
        int Insert(TEntity role);
        string GetRoleName(TKey roleId);
        TKey GetRoleId(string roleName);
        TEntity GetRoleById(TKey roleId);
        TEntity GetRoleByName(string roleName);
        int Update(TEntity role);

    }

    public interface IRoleRepository<TEntity> : IRoleRepository<int, TEntity>
        where TEntity : class, IBaseEntity<int>
    {
    }
}
