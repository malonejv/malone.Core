using malone.Core.EL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace malone.Core.BL.Components.Identity
{
    public class RoleBusinessComponent : RoleBusinessComponent<CoreRole>
    {
        public RoleBusinessComponent(IRoleStore<CoreRole> store, IEnumerable<IRoleValidator<CoreRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<CoreRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }

    public class RoleBusinessComponent<TRoleEntity> : RoleManager<TRoleEntity>
        where TRoleEntity : CoreRole
    {
        public RoleBusinessComponent(IRoleStore<TRoleEntity> store, IEnumerable<IRoleValidator<TRoleEntity>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<TRoleEntity>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
