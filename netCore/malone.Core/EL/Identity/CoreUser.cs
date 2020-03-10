using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using malone.Core.BL.Components.Identity;

namespace malone.Core.EL.Identity
{
    public class CoreUser : User<CoreUserLogin, CoreUserRole, CoreUserClaim>
    {
    }


    public class User<TUserLogin, TUserRole, TUserClaim> : IdentityUser<int, CoreUserLogin, CoreUserRole, CoreUserClaim>, IBaseEntity
        where TUserLogin : CoreUserLogin
        where TUserRole : CoreUserRole
        where TUserClaim : CoreUserClaim
    {
        public User()
        {
            Id = this.GetHashCode();
        }
    }
}
