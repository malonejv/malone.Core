using malone.Core.Identity.Dapper.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;

namespace malone.Core.Identity.Dapper.Business
{
    public class UserManagerConfiguration<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserBC> : IUserManagerConfiguration<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim, TUserBC>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TRoleEntity : CoreRole<TKey, TUserRole>
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TUserBC : UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>
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

        public virtual void ConfigureUserManager(TUserBC userBusinessComponent, IdentityFactoryOptions<UserBusinessComponent> options)
        {
            if (userBusinessComponent == null) throw new ArgumentNullException(nameof(userBusinessComponent));

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {

                //var provider = new DpapiDataProtectionProvider();
                //var entropy = "D4151DA419C4691E";
                //userBusinessComponent.UserTokenProvider = new DataProtectorTokenProvider<TUserEntity, TKey>(provider.Create(entropy))
                //{
                //    TokenLifespan = TimeSpan.FromDays(1)
                //};

                var tokenProvider = TokenProvider<TKey, TUserEntity, TUserLogin, TUserRole, TUserClaim>.Provider;
                userBusinessComponent.UserTokenProvider = tokenProvider;
            }

            userBusinessComponent.EmailService = EmailService;
            userBusinessComponent.SmsService = SmsService;

            //OPTION: Agregar todas estas configuraciones en el web.config

            // Configure validation logic for usernames
            userBusinessComponent.UserValidator = UserValidator;

            // Configure validation logic for passwords
            userBusinessComponent.PasswordValidator = PasswordValidator;

            // Configure user lockout defaults
            userBusinessComponent.UserLockoutEnabledByDefault = true;
            userBusinessComponent.DefaultAccountLockoutTimeSpan = TimeSpan.FromDays(365 * 100); //Tiempo que queda bloqueado 
            userBusinessComponent.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            userBusinessComponent.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<TUserEntity, TKey>
            {
                MessageFormat = "Your security code is {0}"
            });

            userBusinessComponent.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<TUserEntity, TKey>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

        }
    }

    public class UserManagerConfiguration : UserManagerConfiguration<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim, UserBusinessComponent>, IUserManagerConfiguration
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
