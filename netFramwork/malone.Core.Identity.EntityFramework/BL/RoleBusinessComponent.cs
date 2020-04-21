using malone.Core.Identity.EntityFramework.EL;
using Microsoft.AspNet.Identity;
using System;

namespace malone.Core.Identity.EntityFramework
{
    public class RoleBusinessComponent<TKey, TRoleEntity, TUserRole> : RoleManager<TRoleEntity, TKey>
        where TKey : IEquatable<TKey>
        where TUserRole :  CoreUserRole<TKey>
        where TRoleEntity : CoreRole<TKey, TUserRole>, IRole<TKey>
    {
        public RoleBusinessComponent(IRoleStore<TRoleEntity, TKey> store) : base(store)
        {
        }
    }

    public class RoleBusinessComponent : RoleBusinessComponent<int, CoreRole, CoreUserRole>
    {
        public RoleBusinessComponent(IRoleStore<CoreRole, int> store) : base(store)
        {
        }
    }

}
