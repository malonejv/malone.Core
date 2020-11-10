using System;
using malone.Core.Identity.AdoNet.SqlServer.Entities;
using Microsoft.AspNet.Identity.Owin;

namespace malone.Core.Identity.AdoNet.SqlServer.Business
{
    public interface IUserManagerConfiguration<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserBC>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TRoleEntity : CoreRole<TKey, TUserRole>
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TUserBC : UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>
    {
        IEmailMessageService EmailService { get; set; }
        ISmsMessageService SmsService { get; set; }

        void ConfigureUserManager(TUserBC userBusinessComponent, IdentityFactoryOptions<UserBusinessComponent> options);
    }

    public interface IUserManagerConfiguration : IUserManagerConfiguration<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim, UserBusinessComponent>
    {
    }
}
