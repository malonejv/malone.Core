using malone.Core.CL.DI;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.Identity.EntityFramework.EL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;

namespace malone.Core.Identity.EntityFramework
{

    public class UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> : UserManager<TUserEntity, TKey>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TRoleEntity : CoreRole<TKey, TUserRole>
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
    {
        protected IExceptionHandler<CoreErrors> ExceptionHandler { get; }

        public UserBusinessComponent(IUserStore<TUserEntity, TKey> store, IExceptionHandler<CoreErrors> exceptionHandler) : base(store)
        {
            ExceptionHandler = exceptionHandler;
        }

        public static UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> Create(IdentityFactoryOptions<UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>> options, IOwinContext context)
        {
            var instance = ServiceLocator.Current.Get<UserManager<TUserEntity, TKey>>() as UserBusinessComponent<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>;
            return instance;
        }

        //public async Task<TUserEntity> Login(string username, string password, bool rememberMe, string roleName)
        //{
        //    string message = "";

        //    //BusinessValidator.LoginValidationRules()
        //    //    .Add(
        //    //        new ValidationRule()
        //    //        {
        //    //            Method = BusinessValidator.UserExists,
        //    //            Arguments = new List<object>() { username, password }
        //    //        }
        //    //        );
        //    var user = await this.FindAsync(username, password);
        //    if (user != null)
        //    {

        //        if (!await this.IsEmailConfirmedAsync(user.Id))
        //        {
        //            //    message = messageManager.GetDescription((int)CoreErrors.E306);
        //            //    throw new BusinessException((int)CoreErrors.E306, message);
        //        }

        //        if (!await this.IsInRoleAsync(user.Id, roleName))
        //        {
        //            //message = messageManager.GetDescription((int)CoreErrors.E307);
        //            //throw new BusinessException((int)CoreErrors.E307, message);
        //        }
        //    }
        //    else
        //    {
        //        //message = messageManager.GetDescription((int)CoreErrors.E305);
        //        //throw new BusinessException((int)CoreErrors.E305, message);
        //    }

        //    return user;
        //}

        //public override async Task<IdentityResult> CreateAsync(TUserEntity user, string password)
        //{
        //    try
        //    {
        //        var create = await base.CreateAsync(user, password);
        //        return create;
        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}",
        //                    validationError.PropertyName,
        //                    validationError.ErrorMessage);
        //            }
        //        }
        //        return null;
        //    }
        //}
    }

    public class UserBusinessComponent : UserBusinessComponent<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim>
    {
        public UserBusinessComponent(IUserStore<CoreUser, int> store, IExceptionHandler<CoreErrors> exceptionHandler) : base(store, exceptionHandler)
        {
        }

        public static UserBusinessComponent Create(IdentityFactoryOptions<UserBusinessComponent> options, IOwinContext context)
        {
            var instance = ServiceLocator.Current.Get<UserManager<CoreUser, int>>() as UserBusinessComponent;
            return instance;
        }

    }

}
