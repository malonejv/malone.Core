using System;
using malone.Core.DataAccess.Repositories;
using malone.Core.Entities.Model;
using malone.Core.Identity.AdoNet.Entities;

namespace malone.Core.Identity.AdoNet.Repositories
{
    public interface IUserClaimRepository<TKey, TUserClaim> : IRepository<TKey,TUserClaim>
        where TKey : IEquatable<TKey>
        where TUserClaim : CoreUserClaim<TKey>, IBaseEntity<TKey>, new()
    {
    }

    public interface IUserClaimRepository<TUserClaim> : IUserClaimRepository<int, TUserClaim>
        where TUserClaim : CoreUserClaim<int>, IBaseEntity, new()
    {
    }
}
