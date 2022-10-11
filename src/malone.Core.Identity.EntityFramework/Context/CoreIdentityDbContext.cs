using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using malone.Core.DataAccess.Context;
using malone.Core.Identity.EntityFramework.DAL.Mappings;
using malone.Core.Identity.EntityFramework.Entities;
using malone.Core.IoC;
using Microsoft.AspNet.Identity.EntityFramework;

namespace malone.Core.Identity.EntityFramework.Context
{
	public class CoreIdentityDbContext<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim>
		: IdentityDbContext<TUserEntity, TRoleEntity, TKey, TUserLogin, TUserRole, TUserClaim>, IContext
		where TKey : IEquatable<TKey>
		where TUserClaim : CoreUserClaim<TKey>
		where TUserRole : CoreUserRole<TKey>
		where TUserLogin : CoreUserLogin<TKey>
		where TRoleEntity : CoreRole<TKey, TUserRole>
		where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
	{

		public static CoreIdentityDbContext Create()
		{
			var instance = ServiceLocator.Current.Get<IContext>() as CoreIdentityDbContext;
			return instance;
		}


		public CoreIdentityDbContext() : base() { }
		public CoreIdentityDbContext(string nameOrConnectionStringName) : base(nameOrConnectionStringName)
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//TODO: Agregar configuración
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			registerUserIdentityMapping(modelBuilder);
		}

		protected virtual void registerUserIdentityMapping(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new CoreRoleMapping<TRoleEntity, TUserRole, TKey>());
			modelBuilder.Configurations.Add(new CoreUserMapping<TKey, TUserEntity, TUserLogin, TUserRole, TUserClaim>());
			modelBuilder.Configurations.Add(new CoreUserRoleMapping<TKey, TUserRole>());
			modelBuilder.Configurations.Add(new CoreUserLoginMapping<TKey, TUserLogin>());
			modelBuilder.Configurations.Add(new CoreUserClaimMapping<TKey, TUserClaim>());
		}
	}

	public class CoreIdentityDbContext : CoreIdentityDbContext<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim>
	{
		public CoreIdentityDbContext() : base() { }

		public CoreIdentityDbContext(string nameOrConnectionStringName)
			: base(nameOrConnectionStringName)
		{

		}

	}

}
