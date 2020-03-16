using malone.Core.BL.Components.Identity.MessageServices;
using malone.Core.BL.Components.Identity.Validators;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Manager.Implementations;
using malone.Core.EL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace malone.Core.BL.Components.Identity
{

    public class UserBusinessComponent : UserBusinessComponent<CoreUser>
    {
        public UserBusinessComponent(IUserStore<CoreUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<CoreUser> passwordHasher, IEnumerable<IUserValidator<CoreUser>> userValidators, IEnumerable<IPasswordValidator<CoreUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<CoreUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }

    public class UserBusinessComponent<TUserEntity> : UserManager<TUserEntity>
        where TUserEntity : CoreUser
    {
        public UserBusinessComponent(IUserStore<TUserEntity> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<TUserEntity> passwordHasher, IEnumerable<IUserValidator<TUserEntity>> userValidators, IEnumerable<IPasswordValidator<TUserEntity>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<TUserEntity>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            // Configure validation logic for usernames
            this.UserValidators.Add(new CoreUserValidator<TUserEntity>(errors));

            this.PasswordValidators.Add(new PasswordValidator<TUserEntity>(errors));
        }

        public async Task<TUserEntity> Login(string username, string password, bool rememberUser, string roleName)
        {
            var messageManager = new ExceptionMessageManager();
            string message = "";

            var user = await FindByNameAsync(username).ConfigureAwait(false);

            if (user != null)
            {
                if (!await IsEmailConfirmedAsync(user).ConfigureAwait(false))
                {
                    message = messageManager.GetDescription((int)CoreErrors.E306);
                    throw new BusinessException((int)CoreErrors.E306, message);
                }

                if (!await IsInRoleAsync(user, roleName).ConfigureAwait(false))
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
                IdentityResult create = await base.CreateAsync(user, password).ConfigureAwait(false);
                return create;
            }
            catch (Exception) {
                return null;
            }
            //TODO: Ver modo de reemplazar
            //catch (EntityValidationException dbEx)
            //{
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            Trace.TraceInformation("Property: {0} Error: {1}",
            //                validationError.PropertyName,
            //                validationError.ErrorMessage);
            //        }
            //    }
            //    
            //}
        }
    }
}
