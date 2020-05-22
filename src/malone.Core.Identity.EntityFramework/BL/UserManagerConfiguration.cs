﻿using malone.Core.Identity.BL.Components.MessageServices.Interfaces;
using malone.Core.Identity.EntityFramework.EL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;

namespace malone.Core.Identity.EntityFramework.BL
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
        public UserManagerConfiguration(TUserBC userBusinessComponent, IEmailMessageService emailMessageService, ISmsMessageService smsMessageService)
            : base()
        {
            UserBC = userBusinessComponent;
            EmailService = emailMessageService;
            SmsService = smsMessageService;
        }

        public TUserBC UserBC { get; set; }
        public IEmailMessageService EmailService { get; set; }
        public ISmsMessageService SmsService { get; set; }

        public void ConfigureUserManager()
        {
            //OPTION: Agregar todas estas configuraciones en el web.config

            // Configure validation logic for usernames
            UserBC.UserValidator = new UserValidator<TUserEntity, TKey>(UserBC)
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
            UserBC.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<TUserEntity, TKey>
            {
                MessageFormat = "Your security code is {0}"
            });

            UserBC.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<TUserEntity, TKey>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            UserBC.EmailService = EmailService;

            UserBC.SmsService = SmsService;


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
        public UserManagerConfiguration(UserBusinessComponent userBusinessComponent, IEmailMessageService emailMessageService, ISmsMessageService smsMessageService)
            : base(userBusinessComponent, emailMessageService, smsMessageService)
        {
        }
    }
}
