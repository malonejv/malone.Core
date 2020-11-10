using System;
using malone.Core.DataAccess.Repositories;
using malone.Core.Identity.AdoNet.SqlServer.Entities;

namespace malone.Core.Identity.AdoNet.SqlServer.Repositories
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
