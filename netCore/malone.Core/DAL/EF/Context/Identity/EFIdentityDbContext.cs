using malone.Core.DAL.Base.Context;
using malone.Core.DAL.EF.Extensions;
using malone.Core.EL.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        public override int SaveChanges()
        {
            var entities = (from entry in ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added
                            select entry.Entity);

            var validationResults = new List<ValidationResult>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                {
                    //TODO: Ver comportamiento.
                    var test = entity;
                    // throw new ValidationException() or do whatever you want
                }
            }
            return base.SaveChanges();
        }
    }

}
