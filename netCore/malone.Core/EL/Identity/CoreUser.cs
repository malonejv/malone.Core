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
