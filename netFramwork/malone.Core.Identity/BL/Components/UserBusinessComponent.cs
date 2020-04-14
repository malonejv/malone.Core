using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Manager.Implementations;
using malone.Core.Identity.BL.Components.MessageServices;
using malone.Core.Identity.BL.Components.Providers;
using malone.Core.Identity.BL.Components.Validators;
using malone.Core.Identity.EL;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Threading.Tasks;

namespace malone.Core.Identity.BL.Components
{

    public class UserBusinessComponent : UserBusinessComponent<CoreUser>
    {
        public UserBusinessComponent(IUserStore<CoreUser, int> store) : base(store)
        {
        }
    }

    public class UserBusinessComponent<TUserEntity> : UserManager<TUserEntity, int>
        where TUserEntity : CoreUser
    {
        public UserBusinessComponent(IUserStore<TUserEntity, int> store) : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new CoreUserValidator<TUserEntity>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            this.EmailService = new EmailService();

            var provider = new DpapiDataProtectionProvider("TLC");
            var entropy = "D4151DA419C4691E";
            this.UserTokenProvider = new CoreDataProtectorTokenProvider<TUserEntity>(provider.Create(entropy))
            {
                TokenLifespan = TimeSpan.FromDays(1)
            };
        }

        public async Task<TUserEntity> Login(string username, string password, bool rememberUser, string roleName)
        {
            var messageManager = new ExceptionMessageManager();
            string message = "";


            var user = await this.FindAsync(username, password);

            if (user != null)
            {
                if (!await this.IsEmailConfirmedAsync(user.Id))
                {
                    message = messageManager.GetDescription((int)CoreErrors.E306);
                    throw new BusinessException((int)CoreErrors.E306, message);
                }

                if (!await this.IsInRoleAsync(user.Id, roleName))
                {
                    message = messageManager.GetDescription((int)CoreErrors.E307);
                    throw new BusinessException((int)CoreErrors.E307, message);
                }
            }
            else
            {
                message = messageManager.GetDescription((int)CoreErrors.E305);
                throw new BusinessException((int)CoreErrors.E305, message);
            }

            return user;
        }

        public override async Task<IdentityResult> CreateAsync(TUserEntity user, string password)
        {
            try
            {
                var create = await base.CreateAsync(user, password);
                return create;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
                return null;
            }
        }
    }
}
