using malone.Core.EL.Identity;
using Microsoft.AspNetCore.Identity;

namespace malone.Core.BL.Components.Identity.Validators
{
    public class CoreUserValidator<TUserEntity> : UserValidator<TUserEntity>
         where TUserEntity : CoreUser
    {
        public CoreUserValidator(IdentityErrorDescriber errors) : base(errors) { }

    }
}
