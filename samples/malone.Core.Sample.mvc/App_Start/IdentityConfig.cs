
//namespace malone.Core.Sample.mvc
//{
//    Configure the application user manager used in this application.UserManager is defined in ASP.NET Identity and is used by the application.
//    public class UserManagerConfiguration : IUserManagerConfiguration
//    {
//        public UserBusinessComponent(IUserStore<CoreUser> store)
//            : base(store)
//        {
//        }

//        public static UserBusinessComponent Create(IdentityFactoryOptions<UserBusinessComponent> options, IOwinContext context)
//        {
//            var manager = new UserBusinessComponent(new UserStore<CoreUser>(context.Get<ApplicationDbContext>()));
//            // Configure validation logic for usernames
//            manager.UserValidator = new UserValidator<CoreUser>(manager)
//            {
//                AllowOnlyAlphanumericUserNames = false,
//                RequireUniqueEmail = true
//            };

//            // Configure validation logic for passwords
//            manager.PasswordValidator = new PasswordValidator
//            {
//                RequiredLength = 6,
//                RequireNonLetterOrDigit = true,
//                RequireDigit = true,
//                RequireLowercase = true,
//                RequireUppercase = true,
//            };

//            // Configure user lockout defaults
//            manager.UserLockoutEnabledByDefault = true;
//            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
//            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

//            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
//            // You can write your own provider and plug it in here.
//            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<CoreUser>
//            {
//                MessageFormat = "Your security code is {0}"
//            });
//            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<CoreUser>
//            {
//                Subject = "Security Code",
//                BodyFormat = "Your security code is {0}"
//            });
//            manager.EmailService = new EmailService();
//            manager.SmsService = new SmsService();
//            var dataProtectionProvider = options.DataProtectionProvider;
//            if (dataProtectionProvider != null)
//            {
//                manager.UserTokenProvider =
//                    new DataProtectorTokenProvider<CoreUser>(dataProtectionProvider.Create("ASP.NET Identity"));
//            }
//            return manager;
//        }
//    }

//    // Configure the application sign-in manager which is used in this application.
//    public class SignInBusinessComponent : SignInManager<CoreUser, string>
//    {
//        public SignInBusinessComponent(UserBusinessComponent userManager, IAuthenticationManager authenticationManager)
//            : base(userManager, authenticationManager)
//        {
//        }

//        public override Task<ClaimsIdentity> CreateUserIdentityAsync(CoreUser user)
//        {
//            return user.GenerateUserIdentityAsync((UserBusinessComponent)UserManager);
//        }

//        public static SignInBusinessComponent Create(IdentityFactoryOptions<SignInBusinessComponent> options, IOwinContext context)
//        {
//            return new SignInBusinessComponent(context.GetUserManager<UserBusinessComponent>(), context.Authentication);
//        }
//    }
//}
