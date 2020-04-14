using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Identity.EL;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace malone.Core.Identity.BL.Components
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
