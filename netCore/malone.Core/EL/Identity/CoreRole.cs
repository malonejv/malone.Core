using Microsoft.AspNetCore.Identity;

namespace malone.Core.EL.Identity
{
    public class CoreRole : Role<CoreUserRole>
    {
        public CoreRole() : base() {}
    }

    public class Role<TUserRole> : IdentityRole<int>, IBaseEntity
        where TUserRole : CoreUserRole
    {
        public Role()
        {
            Id = this.GetHashCode();
        }
    }
}
