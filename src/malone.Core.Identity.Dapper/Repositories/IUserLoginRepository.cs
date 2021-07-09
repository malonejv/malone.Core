using malone.Core.DataAccess.Repositories;
using malone.Core.Identity.Dapper.Entities;
using System;

namespace malone.Core.Identity.Dapper.Repositories
{
    public interface IUserLoginRepository<TKey, TUserLogin> : IBaseRepository<TUserLogin>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
    {

    }

    public interface IUserLoginRepository<TUserLogin> : IUserLoginRepository<int, TUserLogin>
        where TUserLogin : CoreUserLogin<int>, new()
    {
    }
}
