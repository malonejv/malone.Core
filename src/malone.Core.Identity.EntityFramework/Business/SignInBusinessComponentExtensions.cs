using System;
using malone.Core.Async.Threading;
using malone.Core.Identity.EntityFramework.Entities;
using Microsoft.AspNet.Identity.Owin;

namespace malone.Core.Identity.EntityFramework.Business
	{
	public static class SignInServiceExtensions
    {
        public static SignInStatus PasswordUserNameOrEmailSignIn<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserManager>(this SignInService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserManager> signInManager, string email, string password, bool isPersistent, bool shouldLockout)
            where TKey : IEquatable<TKey>
            where TUserLogin : CoreUserLogin<TKey>, new()
            where TUserRole : CoreUserRole<TKey>, new()
            where TUserClaim : CoreUserClaim<TKey>, new()
            where TRoleEntity : CoreRole<TKey, TUserRole>, new()
            where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>, new()
            where TUserManager : UserService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>
        {
            if (signInManager == null)
            {
                throw new ArgumentNullException(nameof(signInManager));
            }

            return AsyncHelper.RunSync(() => signInManager.PasswordUserNameOrEmailSignInAsync(email, password, isPersistent, shouldLockout));
        }

        public static SignInStatus PasswordUserNameOrEmailSignIn(this SignInService signInManager, string email, string password, bool isPersistent, bool shouldLockout)
        {
            if (signInManager == null)
            {
                throw new ArgumentNullException(nameof(signInManager));
            }

            return AsyncHelper.RunSync(() => signInManager.PasswordUserNameOrEmailSignInAsync(email, password, isPersistent, shouldLockout));
        }
    }
}
