using malone.Core.EL.Identity;

namespace malone.Core.BL.Components.Identity
{
    public class SignInBusinessComponent : SignInBusinessComponent<CoreUser>
    {
        public SignInBusinessComponent(UserManager<CoreUser, int> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }
    }
    // Configure the application sign-in manager which is used in this application.
    public class SignInBusinessComponent<TUserEntity> : SignInManager<TUserEntity, int>
        where TUserEntity : CoreUser
    {
        public SignInBusinessComponent(UserManager<TUserEntity, int> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }
    }
}
