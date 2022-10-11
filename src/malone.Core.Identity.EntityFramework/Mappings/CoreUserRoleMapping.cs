using System;
using System.Data.Entity.ModelConfiguration;
using malone.Core.Identity.EntityFramework.Entities;

namespace malone.Core.Identity.EntityFramework.DAL.Mappings
{
	public class CoreUserRoleMapping<TKey, TUserRole> : EntityTypeConfiguration<TUserRole>
		where TKey : IEquatable<TKey>
		where TUserRole : CoreUserRole<TKey>
	{
		public CoreUserRoleMapping()
		{
			ToTable("UsersRoles")
				.HasKey((TUserRole r) => new
				{
					r.UserId,
					r.RoleId
				});

		}
	}
}
