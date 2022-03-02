using System;
using System.Threading.Tasks;
using malone.Core.Commons.Exceptions;
using malone.Core.Identity.Dapper.Entities;
using malone.Core.IoC;
using malone.Core.Logging;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace malone.Core.Identity.Dapper.Business
	{
	// Configure the application sign-in manager which is used in this application.
	public class SignInService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserManager> : SignInManager<TUserEntity, TKey>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TRoleEntity : CoreRole<TKey, TUserRole>, new()
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>, new()
        where TUserManager : UserService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>
    {
        public ICoreLogger Logger { get; set; }

        public SignInService(TUserManager userManager, IAuthenticationManager authenticationManager, ICoreLogger logger) : base(userManager, authenticationManager)
        {
            Logger = logger;
        }


        public static SignInService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserManager> Create(IdentityFactoryOptions<SignInService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserManager>> options, IOwinContext context)
        {
            var logger = ServiceLocator.Current.Get<ICoreLogger>();
            var userManager = context.GetUserManager<TUserManager>();
            var instance = new SignInService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserManager>(userManager, context.Authentication, logger);
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

    public class SignInService : SignInService<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim, UserService>
    {
        public SignInService(UserService userManager, IAuthenticationManager authenticationManager, ICoreLogger logger) : base(userManager, authenticationManager, logger)
        {
        }

        public static SignInService Create(IdentityFactoryOptions<SignInService> options, IOwinContext context)
        {
            var logger = ServiceLocator.Current.Get<ICoreLogger>();
            var userManager = context.GetUserManager<UserService>();
            var instance = new SignInService(userManager, context.Authentication, logger);

            return instance;
        }
    }

}
