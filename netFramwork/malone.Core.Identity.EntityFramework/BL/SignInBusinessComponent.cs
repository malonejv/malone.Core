using malone.Core.Identity.EntityFramework.EL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;

namespace malone.Core.Identity.EntityFramework
{
    // Configure the application sign-in manager which is used in this application.
    public class SignInBusinessComponent<TKey, TUserEntity, TUserLogin, TUserRole, TUserClaim> : SignInManager<TUserEntity, TKey>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>
        where TUserRole : CoreUserRole<TKey>
        where TUserClaim : CoreUserClaim<TKey>
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
    {
        public SignInBusinessComponent(UserManager<TUserEntity, TKey> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }
    }

    public class SignInBusinessComponent : SignInBusinessComponent<int, CoreUser, CoreUserLogin, CoreUserRole, CoreUserClaim>
    {
        public SignInBusinessComponent(UserManager<CoreUser, int> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }
    }

}
