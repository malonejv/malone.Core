using malone.Core.Identity;
using malone.Core.Identity.AdoNet.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;

namespace malone.Core.Identity.AdoNet.Business
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
            TUserBC userBusinessComponent,
            IEmailMessageService emailMessageService,
            ISmsMessageService smsMessageService,
            IIdentityValidator<TUserEntity> userValidator,
            IIdentityValidator<string> passwordValidator
            )
            : base()
        {
            UserBC = userBusinessComponent;
            EmailService = emailMessageService;
            SmsService = smsMessageService;
            UserValidator = userValidator;
            PasswordValidator = passwordValidator;
        }

        public TUserBC UserBC { get; set; }
        public IEmailMessageService EmailService { get; set; }
        public ISmsMessageService SmsService { get; set; }
        public IIdentityValidator<TUserEntity> UserValidator { get; set; }
        public IIdentityValidator<string> PasswordValidator { get; set; }

        public virtual void ConfigureUserManager()
        {

            UserBC.EmailService = EmailService;

            UserBC.SmsService = SmsService;

            //OPTION: Agregar todas estas configuraciones en el web.config

            // Configure validation logic for usernames
            UserBC.UserValidator = UserValidator;

            // Configure validation logic for passwords
            UserBC.PasswordValidator = PasswordValidator;

            // Configure user lockout defaults
            UserBC.UserLockoutEnabledByDefault = true;
            UserBC.DefaultAccountLockoutTimeSpan = TimeSpan.FromDays(365 * 100); //Tiempo que queda bloqueado 
            UserBC.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            UserBC.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<TUserEntity, TKey>
            {
                MessageFormat = "Your security code is {0}"
            });

            UserBC.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<TUserEntity, TKey>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            //TODO: Revisar

            var provider = new DpapiDataProtectionProvider("Sample");
            var entropy = "D4151DA419C4691E";
            UserBC.UserTokenProvider = new DataProtectorTokenProvider<TUserEntity, TKey>(provider.Create(entropy))
            {
                TokenLifespan = TimeSpan.FromDays(1)
            };
        }
    }
    public class UserManagerConfiguration : UserManagerConfiguration<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim, UserBusinessComponent>, IUserManagerConfiguration
    {
        public UserManagerConfiguration(
            UserBusinessComponent userBusinessComponent,
            IEmailMessageService emailMessageService,
            ISmsMessageService smsMessageService,
            IIdentityValidator<CoreUser> userValidator,
            IIdentityValidator<string> passwordValidator)
            : base(userBusinessComponent, emailMessageService, smsMessageService, userValidator, passwordValidator)
        {
        }
    }
}
