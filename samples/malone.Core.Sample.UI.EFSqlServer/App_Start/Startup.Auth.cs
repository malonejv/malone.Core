using malone.Core.Commons.DI;
using malone.Core.Identity.EntityFramework;
using malone.Core.Identity.EntityFramework.Business;
using malone.Core.Identity.EntityFramework.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using System;

namespace malone.Core.Sample.UI.EFSqlServer
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            //OPTION: Configuracion
            //app.CreatePerOwinContext(SampleEFContext.Create);
            app.CreatePerOwinContext<UserBusinessComponent>(UserBusinessComponent.Create);
            app.CreatePerOwinContext<SignInBusinessComponent>(SignInBusinessComponent.Create);

            var userManagerConfiguration = ServiceLocator.Current.Get<IUserManagerConfiguration>();
            userManagerConfiguration.ConfigureUserManager();


            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "496153604649-th4n8cedqb89qt506elg7har6e3nb7sd.apps.googleusercontent.com",
                ClientSecret = "mZMkvcSbkBUbGEylO2K1SEog"
            });

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserBusinessComponent, CoreUser, int>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie),
                        getUserIdCallback: claims => claims.GetUserId<int>()
                    )
                }
            });
        }

    }

    //public class UserManagerConfiguration : IUserManagerConfiguration
    //{
    //    public UserBusinessComponent UserBC { get; set; }
    //    public IEmailMessageService EmailService { get; set; }
    //    public ISmsMessageService SmsService { get; set; }

    //    public UserManagerConfiguration(UserBusinessComponent userBusinessComponent, IEmailMessageService emailMessageService, ISmsMessageService smsMessageService)
    //    {
    //        UserBC = userBusinessComponent;
    //        EmailService = emailMessageService;
    //        SmsService = smsMessageService;

    //        //UserBC = ServiceLocator.Current.Get<UserManager<CoreUser, int>>() as UserBusinessComponent;
    //        //EmailService = ServiceLocator.Current.Get<IEmailMessageService>();
    //        //SmsService = ServiceLocator.Current.Get<ISmsMessageService>();
    //    }

    //    public void ConfigureUserManager()
    //    {
    //        //OPTION: Agregar todas estas configuraciones en el web.config

    //        // Configure validation logic for usernames
    //        UserBC.UserValidator = new UserValidator<CoreUser, int>(UserBC)
    //        {
    //            AllowOnlyAlphanumericUserNames = false,
    //            RequireUniqueEmail = true
    //        };

    //        // Configure validation logic for passwords
    //        UserBC.PasswordValidator = new PasswordValidator
    //        {
    //            RequiredLength = 6,
    //            RequireNonLetterOrDigit = true,
    //            RequireDigit = true,
    //            RequireLowercase = true,
    //            RequireUppercase = true,
    //        };

    //        // Configure user lockout defaults
    //        UserBC.UserLockoutEnabledByDefault = true;
    //        UserBC.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //        UserBC.MaxFailedAccessAttemptsBeforeLockout = 5;

    //        // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
    //        // You can write your own provider and plug it in here.
    //        UserBC.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<CoreUser, int>
    //        {
    //            MessageFormat = "Your security code is {0}"
    //        });

    //        UserBC.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<CoreUser, int>
    //        {
    //            Subject = "Security Code",
    //            BodyFormat = "Your security code is {0}"
    //        });

    //        UserBC.EmailService = EmailService;

    //        UserBC.SmsService = SmsService;


    //        var provider = new DpapiDataProtectionProvider("Sample");
    //        var entropy = "D4151DA419C4691E";
    //        UserBC.UserTokenProvider = new DataProtectorTokenProvider<CoreUser, int>(provider.Create(entropy))
    //        {
    //            TokenLifespan = TimeSpan.FromDays(1)
    //        };
    //    }
    //}
}