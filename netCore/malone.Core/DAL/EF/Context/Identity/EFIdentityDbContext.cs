using malone.Core.DAL.Base.Context;
using malone.Core.EL.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace malone.Core.DAL.EF.Context.Identity
{
    public class EFIdentityDbContext : EFIdentityDbContext<CoreUser, CoreRole>
    {
        public EFIdentityDbContext(string nameOrConnectionStringName) : base(nameOrConnectionStringName)
        {
        }
    }


    public class EFIdentityDbContext<TUserEntity, TRoleEntity> : IdentityDbContext<TUserEntity, TRoleEntity, int, CoreUserLogin, CoreUserRole, CoreUserClaim>, IEFContext
        where TUserEntity : CoreUser
        where TRoleEntity : CoreRole
    {
        public EFIdentityDbContext(string nameOrConnectionStringName)
            : base(nameOrConnectionStringName)
        {

        }

        DbSet<TEntity> IEFContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        DbEntityEntry<TEntity> IEFContext.Entry<TEntity>(TEntity entity)
        {
            return base.Entry<TEntity>(entity);
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

            EntityTypeConfiguration <CoreUser> entityUserConfiguration = modelBuilder.Entity<CoreUser>();
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
    
}
