using malone.Core.EL.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace malone.Core.Identity.EL
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
