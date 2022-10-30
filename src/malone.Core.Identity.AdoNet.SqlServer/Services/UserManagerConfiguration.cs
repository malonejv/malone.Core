using System;
using malone.Core.Identity.AdoNet.SqlServer.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace malone.Core.Identity.AdoNet.SqlServer.Services
{
	public class UserManagerConfiguration<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserBC> : IUserManagerConfiguration<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserBC>
		where TKey : IEquatable<TKey>
		where TUserLogin : CoreUserLogin<TKey>, new()
		where TUserRole : CoreUserRole<TKey>, new()
		where TUserClaim : CoreUserClaim<TKey>, new()
		where TRoleEntity : CoreRole<TKey, TUserRole>
		where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
		where TUserBC : UserService<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>, new()
	{
		public UserManagerConfiguration(
			IEmailMessageService emailMessageService,
			ISmsMessageService smsMessageService,
			IIdentityValidator<TUserEntity> userValidator,
			IIdentityValidator<string> passwordValidator
			)
			: base()
		{
			EmailService = emailMessageService;
			SmsService = smsMessageService;
			UserValidator = userValidator;
			PasswordValidator = passwordValidator;
		}

		public IEmailMessageService EmailService { get; set; }
		public ISmsMessageService SmsService { get; set; }
		public IIdentityValidator<TUserEntity> UserValidator { get; set; }
		public IIdentityValidator<string> PasswordValidator { get; set; }

		public virtual void ConfigureUserManager(TUserBC userService, IdentityFactoryOptions<UserService> options)
		{
			if (userService == null)
			{
				throw new ArgumentNullException(nameof(userService));
			}

			var dataProtectionProvider = options.DataProtectionProvider;

			if (dataProtectionProvider != null)
			{

				//var provider = new DpapiDataProtectionProvider();
				//var entropy = "D4151DA419C4691E";
				//userService.UserTokenProvider = new DataProtectorTokenProvider<TUserEntity, TKey>(provider.Create(entropy))
				//{
				//    TokenLifespan = TimeSpan.FromDays(1)
				//};

				var tokenProvider = TokenProvider<TKey, TUserEntity, TUserLogin, TUserRole, TUserClaim>.Provider;
				userService.UserTokenProvider = tokenProvider;
			}

			userService.EmailService = EmailService;
			userService.SmsService = SmsService;

			//OPTION: Agregar todas estas configuraciones en el web.config

			// Configure validation logic for usernames
			userService.UserValidator = UserValidator;

			// Configure validation logic for passwords
			userService.PasswordValidator = PasswordValidator;

			// Configure user lockout defaults
			userService.UserLockoutEnabledByDefault = true;
			userService.DefaultAccountLockoutTimeSpan = TimeSpan.FromDays(365 * 100); //Tiempo que queda bloqueado
			userService.MaxFailedAccessAttemptsBeforeLockout = 5;

			// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
			// You can write your own provider and plug it in here.
			userService.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<TUserEntity, TKey>
			{
				MessageFormat = "Your security code is {0}"
			});

			userService.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<TUserEntity, TKey>
			{
				Subject = "Security Code",
				BodyFormat = "Your security code is {0}"
			});

		}
	}

	public class UserManagerConfiguration : UserManagerConfiguration<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim, UserService>, IUserManagerConfiguration
	{
		public UserManagerConfiguration(
			IEmailMessageService emailMessageService,
			ISmsMessageService smsMessageService,
			IIdentityValidator<CoreUser> userValidator,
			IIdentityValidator<string> passwordValidator)
			: base(emailMessageService, smsMessageService, userValidator, passwordValidator)
		{
		}
	}
}
