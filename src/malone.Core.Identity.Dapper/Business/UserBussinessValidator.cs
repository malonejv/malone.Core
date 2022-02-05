using System;
using malone.Core.Business.Components;
using malone.Core.Identity.Dapper.Entities;

namespace malone.Core.Identity.Dapper.Business
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
