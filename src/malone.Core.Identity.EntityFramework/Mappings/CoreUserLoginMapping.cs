using System;
using System.Data.Entity.ModelConfiguration;
using malone.Core.Identity.EntityFramework.Entities;

namespace malone.Core.Identity.EntityFramework.DAL.Mappings
{
	public class CoreUserLoginMapping<TKey, TUserLogin> : EntityTypeConfiguration<TUserLogin>
		where TKey : IEquatable<TKey>
		where TUserLogin : CoreUserLogin<TKey>
	{
		public CoreUserLoginMapping()
		{
			ToTable("UsersLogins")
				.HasKey((TUserLogin l) => new
				{
					l.LoginProvider,
					l.ProviderKey,
					l.UserId
				});

		}
	}
}
