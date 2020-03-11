using malone.Core.EL.Identity;

namespace malone.Core.BL.Components.Identity.Validators
{
    public class CoreUserValidator<TUserEntity> : UserValidator<TUserEntity, int>
         where TUserEntity : CoreUser
    {
        public CoreUserValidator(UserManager<TUserEntity, int> manager) : base(manager) { }

    }
}
