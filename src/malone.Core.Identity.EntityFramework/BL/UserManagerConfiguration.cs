using malone.Core.Identity.BL.Components.MessageServices.Interfaces;
using malone.Core.Identity.EntityFramework.EL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;

namespace malone.Core.Identity.EntityFramework.BL
{
    public class UserManagerConfiguration : IUserManagerConfiguration
    {
        public UserBusinessComponent UserBC { get; set; }
        public IEmailMessageService EmailService { get; set; }
        public ISmsMessageService SmsService { get; set; }

        public UserManagerConfiguration(UserBusinessComponent userBusinessComponent, IEmailMessageService emailMessageService, ISmsMessageService smsMessageService)
        {
            UserBC = userBusinessComponent;
            EmailService = emailMessageService;
            SmsService = smsMessageService;

            //UserBC = ServiceLocator.Current.Get<UserManager<CoreUser, int>>() as UserBusinessComponent;
            //EmailService = ServiceLocator.Current.Get<IEmailMessageService>();
            //SmsService = ServiceLocator.Current.Get<ISmsMessageService>();
        }

        public void ConfigureUserManager()
        {
            //OPTION: Agregar todas estas configuraciones en el web.config

            // Configure validation logic for usernames
            UserBC.UserValidator = new UserValidator<CoreUser, int>(UserBC)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            UserBC.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            UserBC.UserLockoutEnabledByDefault = true;
            UserBC.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            UserBC.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            UserBC.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<CoreUser, int>
            {
                MessageFormat = "Your security code is {0}"
            });

            UserBC.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<CoreUser, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            UserBC.EmailService = EmailService;

            UserBC.SmsService = SmsService;


            var provider = new DpapiDataProtectionProvider("Sample");
            var entropy = "D4151DA419C4691E";
            UserBC.UserTokenProvider = new DataProtectorTokenProvider<CoreUser, int>(provider.Create(entropy))
            {
                TokenLifespan = TimeSpan.FromDays(1)
            };
        }
    }
}
