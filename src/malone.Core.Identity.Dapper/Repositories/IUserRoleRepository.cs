using malone.Core.DataAccess.Repositories;
using malone.Core.Identity.Dapper.Entities;
using System;

namespace malone.Core.Identity.Dapper.Repositories
{
    public interface IUserRoleRepository<TKey, TUserRole> : IBaseRepository<TUserRole>
        where TKey : IEquatable<TKey>
        where TUserRole : CoreUserRole<TKey>, new()
    {
    }

    public interface IUserRoleRepository<TUserRole> : IUserRoleRepository<int, TUserRole>
        where TUserRole : CoreUserRole<int>, new()
    {
    }
}
