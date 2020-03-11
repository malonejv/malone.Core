using malone.Core.EL.Identity;

namespace malone.Core.BL.Components.Identity
{
    public class RoleBusinessComponent : RoleBusinessComponent<CoreRole>
    {
        public RoleBusinessComponent(IRoleStore<CoreRole, int> store) : base(store)
        {
        }
    }

    public class RoleBusinessComponent<TRoleEntity> : RoleManager<TRoleEntity, int>
        where TRoleEntity : CoreRole
    {
        public RoleBusinessComponent(IRoleStore<TRoleEntity, int> store) : base(store)
        {
        }
    }
}
