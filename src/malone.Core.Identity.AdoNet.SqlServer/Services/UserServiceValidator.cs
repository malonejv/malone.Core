using System;
using malone.Core.Identity.AdoNet.SqlServer.Entities;
using malone.Core.Services;

namespace malone.Core.Identity.AdoNet.SqlServer.Services
	{
	public class UserBussinessValidator<TKey, TUserEntity, TUserLogin, TUserRole, TUserClaim> : ServiceValidator<TKey, TUserEntity>
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
