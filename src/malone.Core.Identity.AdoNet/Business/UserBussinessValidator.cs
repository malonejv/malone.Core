using malone.Core.Business.Components;
using malone.Core.Sample.AdoNet.Entities;
using System;

namespace malone.Core.Identity.AdoNet.Business
{
    public class UserBussinessValidator<TKey, TUserEntity, TUserLogin, TUserRole, TUserClaim> : BusinessValidator<TKey, TUserEntity>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
    {

        public UserBussinessValidator() : base()
        {
        }
    }
}
