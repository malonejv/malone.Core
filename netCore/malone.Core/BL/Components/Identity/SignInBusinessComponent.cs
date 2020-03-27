using malone.Core.EL.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace malone.Core.BL.Components.Identity
{
    public class SignInBusinessComponent : SignInBusinessComponent<CoreUser>
    {
        public SignInBusinessComponent(UserManager<CoreUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<CoreUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<CoreUser>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }
    }
    // Configure the application sign-in manager which is used in this application.
    public class SignInBusinessComponent<TUserEntity> : SignInManager<TUserEntity>
        where TUserEntity : CoreUser
    {
        public SignInBusinessComponent(UserManager<TUserEntity> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<TUserEntity> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<TUserEntity>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }
    }
}
