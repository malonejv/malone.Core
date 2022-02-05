using System;
using System.Threading.Tasks;
using malone.Core.Commons.DI;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Log;
using malone.Core.Identity.EntityFramework.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

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
        public ILogger Logger { get; set; }

        public SignInBusinessComponent(TUserManager userManager, IAuthenticationManager authenticationManager, ILogger logger) : base(userManager, authenticationManager)
        {
            Logger = logger;
        }


        public static SignInBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserManager> Create(IdentityFactoryOptions<SignInBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserManager>> options, IOwinContext context)
        {
            var logger = ServiceLocator.Current.Get<ILogger>();
            var userManager = context.GetUserManager<TUserManager>();
            var instance = new SignInBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserManager>(userManager, context.Authentication, logger);
            return instance;
        }

        public async Task<SignInStatus> PasswordUserNameOrEmailSignInAsync(string userNameOrEmail, string password, bool isPersistent, bool shouldLockout)
        {
            if (string.IsNullOrEmpty(userNameOrEmail))
            {
                throw new ArgumentNullException(nameof(userNameOrEmail));
            }

            try
            {
                if (userNameOrEmail.Contains("@"))
                {
                    var user = await UserManager.FindByEmailAsync(userNameOrEmail);
                    return await PasswordSignInAsync(user.UserName, password, isPersistent, shouldLockout);
                }
                else
                {
                    return await PasswordSignInAsync(userNameOrEmail, password, isPersistent, shouldLockout);
                }
            }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = CoreExceptionFactory.CreateException<TechnicalException>(ex, CoreErrors.TECH201);
                if (Logger != null)
                {
                    Logger.Error(techEx);
                }

                throw techEx;
            }
        }

    }

    public class SignInBusinessComponent : SignInBusinessComponent<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim, UserBusinessComponent>
    {
        public SignInBusinessComponent(UserBusinessComponent userManager, IAuthenticationManager authenticationManager, ILogger logger) : base(userManager, authenticationManager, logger)
        {
        }

        public static SignInBusinessComponent Create(IdentityFactoryOptions<SignInBusinessComponent> options, IOwinContext context)
        {
            var logger = ServiceLocator.Current.Get<ILogger>();
            var userManager = context.GetUserManager<UserBusinessComponent>();
            var instance = new SignInBusinessComponent(userManager, context.Authentication, logger);

            return instance;
        }
    }

}
