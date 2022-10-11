using System;
using malone.Core.Identity.Dapper.Entities;
using Microsoft.AspNet.Identity;

namespace malone.Core.Identity.Dapper.Business
{
	public class RoleService<TKey, TRoleEntity, TUserRole> : RoleManager<TRoleEntity, TKey>
		where TKey : IEquatable<TKey>
		where TUserRole : CoreUserRole<TKey>, new()
		where TRoleEntity : CoreRole<TKey, TUserRole>, IRole<TKey>, new()
	{
		public RoleService(IRoleStore<TRoleEntity, TKey> store) : base(store)
		{
		}
	}

	public class RoleService : RoleService<int, CoreRole, CoreUserRole>
	{
		public RoleService(IRoleStore<CoreRole, int> store) : base(store)
		{
		}
	}

}
