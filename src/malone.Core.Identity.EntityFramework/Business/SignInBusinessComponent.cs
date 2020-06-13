using malone.Core.Identity.EntityFramework.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;

namespace malone.Core.Identity.EntityFramework
{
    // Configure the application sign-in manager which is used in this application.
    public class SignInBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserManager> : SignInManager<TUserEntity, TKey>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TRoleEntity : CoreRole<TKey, TUserRole>, new()
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>, new()
        where TUserManager : UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>
    {
        public SignInBusinessComponent(TUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }


        public static SignInBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserManager> Create(IdentityFactoryOptions<SignInBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserManager>> options, IOwinContext context)
        {
            var instance = new SignInBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserManager>(
                context.GetUserManager<TUserManager>(), context.Authentication);
            return instance;
        }

    }

    public class SignInBusinessComponent : SignInBusinessComponent<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim, UserBusinessComponent>
    {
        public SignInBusinessComponent(UserBusinessComponent userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public static SignInBusinessComponent Create(IdentityFactoryOptions<SignInBusinessComponent> options, IOwinContext context)
        {
            var instance = new SignInBusinessComponent(
                context.GetUserManager<UserBusinessComponent>(), context.Authentication);
            return instance;
        }
    }

}
