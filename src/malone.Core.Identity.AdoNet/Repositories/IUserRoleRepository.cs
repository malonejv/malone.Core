using System;
using malone.Core.DataAccess.Repositories;
using malone.Core.Identity.AdoNet.Entities;

namespace malone.Core.Identity.AdoNet.Repositories
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
