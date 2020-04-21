using malone.Core.DAL.Context;
using malone.Core.DAL.EF.Context;
using malone.Core.Identity.EntityFramework.EL;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace malone.Core.Identity.EntityFramework.DAL.EF.Context
{
    public class EFIdentityDbContext<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> : IdentityDbContext<TUserEntity, TRoleEntity, TKey, TUserLogin, TUserRole, TUserClaim>, IEFContext
        where TKey : IEquatable<TKey>
        where TUserClaim : CoreUserClaim<TKey>
        where TUserRole : CoreUserRole<TKey>
        where TUserLogin : CoreUserLogin<TKey>
        where TRoleEntity : CoreRole<TKey, TUserRole>
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
    {
        public EFIdentityDbContext(string nameOrConnectionStringName) : base(nameOrConnectionStringName)
        {
        }

        protected virtual void registerUserIdentityMapping(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<CoreRole> entityRoleConfiguration = modelBuilder.Entity<CoreRole>();
            entityRoleConfiguration.ToTable("Roles");
            StringPropertyConfiguration indexRoleName = entityRoleConfiguration.Property((CoreRole r) => r.Name).IsRequired().HasMaxLength(new int?(256));
            string roleNameIndexColumnName = "Index";
            IndexAttribute indexRolaNameAttribute = new IndexAttribute("RoleNameIndex");
            indexRolaNameAttribute.IsUnique = true;
            indexRoleName.HasColumnAnnotation(roleNameIndexColumnName, new IndexAnnotation(indexRolaNameAttribute));
            entityRoleConfiguration.HasMany<CoreUserRole>((CoreRole r) => (ICollection<CoreUserRole>)r.Users).WithRequired().HasForeignKey<int>((CoreUserRole ur) => ur.RoleId);

            EntityTypeConfiguration<CoreUser> entityUserConfiguration = modelBuilder.Entity<CoreUser>();
            entityUserConfiguration.ToTable("Users");
            entityUserConfiguration.Property(p => p.Id);
            entityUserConfiguration.HasMany<CoreUserRole>((CoreUser u) => (ICollection<CoreUserRole>)u.Roles).WithOptional().HasForeignKey<int>((CoreUserRole ur) => ur.UserId);
            entityUserConfiguration.HasMany<CoreUserClaim>((CoreUser u) => (ICollection<CoreUserClaim>)u.Claims).WithOptional().HasForeignKey<int>((CoreUserClaim uc) => uc.UserId);
            entityUserConfiguration.HasMany<CoreUserLogin>((CoreUser u) => (ICollection<CoreUserLogin>)u.Logins).WithRequired().HasForeignKey<int>((CoreUserLogin ul) => ul.UserId);
            StringPropertyConfiguration indexUserName = entityUserConfiguration.Property((CoreUser u) => u.UserName).IsRequired().HasMaxLength(new int?(256));
            string userNameIndexColumnName = "Index";
            IndexAttribute indexUserNameAttribute = new IndexAttribute("UserNameIndex");
            indexUserNameAttribute.IsUnique = true;
            indexUserName.HasColumnAnnotation(userNameIndexColumnName, new IndexAnnotation(indexUserNameAttribute));
            entityUserConfiguration.Property((CoreUser u) => u.Email).HasMaxLength(new int?(256));

            modelBuilder.Entity<CoreUserRole>().ToTable("UsersRoles")
                .HasKey((CoreUserRole r) => new
                {
                    r.UserId,
                    r.RoleId
                });

            modelBuilder.Entity<CoreUserLogin>().ToTable("UsersLogins")
                .HasKey((CoreUserLogin l) => new
                {
                    l.LoginProvider,
                    l.ProviderKey,
                    l.UserId
                });

            modelBuilder.Entity<CoreUserClaim>().ToTable("UsersClaims");

        }
    }

    //public class EFIdentityDbContext<TUserEntity, TRoleEntity> : EFIdentityDbContext<int, TUserEntity, TRoleEntity, CoreUserLogin, CoreUserRole, CoreUserClaim>
    //    where TRoleEntity : CoreRole<CoreUserRole>
    //    where TUserEntity : CoreUser
    //{
    //    public EFIdentityDbContext(string nameOrConnectionStringName) : base(nameOrConnectionStringName)
    //    {
    //    }

    //}

    public class EFIdentityDbContext : EFIdentityDbContext<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim>
    {
        public EFIdentityDbContext(string nameOrConnectionStringName)
            : base(nameOrConnectionStringName)
        {

        }

    }

}
