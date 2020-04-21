using Microsoft.AspNet.Identity;

namespace malone.Core.Identity.BL.Components.Validators
{
    public class CoreUserValidator<TUserEntity> : UserValidator<TUserEntity, int>
         where TUserEntity : CoreUser
    {
        public CoreUserValidator(UserManager<TUserEntity, int> manager) : base(manager) { }

    }
}
