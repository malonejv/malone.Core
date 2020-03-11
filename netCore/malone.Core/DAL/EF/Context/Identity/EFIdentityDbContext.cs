using malone.Core.DAL.Base.Context;
using malone.Core.DAL.EF.Extensions;
using malone.Core.EL.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace malone.Core.DAL.EF.Context.Identity
{
    public class EFIdentityDbContext : EFIdentityDbContext<CoreUser, CoreRole>
    {
        public EFIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }


    public class EFIdentityDbContext<TUserEntity, TRoleEntity> : IdentityDbContext<TUserEntity, TRoleEntity, int, CoreUserClaim, CoreUserRole, CoreUserLogin, CoreRoleClaim, CoreUserToken>, IEFContext
        where TUserEntity : CoreUser
        where TRoleEntity : CoreRole
    {
        public EFIdentityDbContext(DbContextOptions options)
            : base(options)
        {

        }

        DbSet<TEntity> IEFContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        EntityEntry<TEntity> IEFContext.Entry<TEntity>(TEntity entity)
        {
            return base.Entry<TEntity>(entity);
        }


        protected virtual void registerUserIdentityMapping(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.AddConfiguration(new CoreRoleConfiguration());
            modelBuilder.AddConfiguration(new CoreUserConfiguration());
            modelBuilder.AddConfiguration(new CoreUserRolesConfiguration());
            modelBuilder.AddConfiguration(new CoreUserLoginsConfiguration());
            modelBuilder.AddConfiguration(new CoreUserClaimsConfiguration());
        }
    }

}
