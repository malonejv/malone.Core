using malone.Core.DataAccess.Repositories;
using malone.Core.Identity.Dapper.Entities;
using System;
using System.Collections.Generic;

namespace malone.Core.Identity.Dapper.Repositories
{
    public interface IUserRoleRepository<TKey, TUserRole> : ICustomRepository<TUserRole>
        where TKey : IEquatable<TKey>
        where TUserRole : CoreUserRole<TKey>, new()
    {
        List<string> FindByUserId<TUserKey>(TUserKey userId)
            where TUserKey : IEquatable<TUserKey>;
        int Delete<TUserKey, TRoleKey>(TUserKey userId, TRoleKey roleId)
            where TUserKey : IEquatable<TUserKey>
            where TRoleKey : IEquatable<TRoleKey>;
        int Insert<TUserKey, TRoleKey>(TUserKey userId, TRoleKey roleId)
            where TUserKey : IEquatable<TUserKey>
            where TRoleKey : IEquatable<TRoleKey>;
    }

    public interface IUserRoleRepository<TUserRole> : IUserRoleRepository<int, TUserRole>
        where TUserRole : CoreUserRole<int>, new()
    {
    }
}
