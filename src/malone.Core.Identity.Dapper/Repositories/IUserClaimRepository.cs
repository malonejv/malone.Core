using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Model;
using malone.Core.Identity.Dapper.Entities;
using System;
using System.Security.Claims;

namespace malone.Core.Identity.Dapper.Repositories
{
    public interface IUserClaimRepository<TKey, TUserClaim> : ICustomRepository<TUserClaim>
        where TKey : IEquatable<TKey>
        where TUserClaim : CoreUserClaim<TKey>, IBaseEntity<TKey>, new()
    {
        ClaimsIdentity FindByUserId<TUserKey>(TUserKey userId)
            where TUserKey : IEquatable<TUserKey>;
        int Delete<TUserKey>(TUserKey userId)
            where TUserKey : IEquatable<TUserKey>;
        int Insert<TUserKey>(TUserKey userId, Claim userClaim)
            where TUserKey : IEquatable<TUserKey>;
        int Delete<TUserKey>(TUserKey userId, Claim claim)
            where TUserKey : IEquatable<TUserKey>;

    }

    public interface IUserClaimRepository<TUserClaim> : IUserClaimRepository<int, TUserClaim>
        where TUserClaim : CoreUserClaim, IBaseEntity, new()
    {
    }
}
