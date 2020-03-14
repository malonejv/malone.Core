namespace malone.Core.EL.Identity
{
    public class CoreRole : Role<CoreUserRole>
    {
        public CoreRole() : base() {}
    }

    public class Role<TUserRole> : IdentityRole<int, TUserRole>, IBaseEntity
        where TUserRole : CoreUserRole
    {
        public Role()
        {
            Id = this.GetHashCode();
        }
    }
}
