using malone.Core.EF.Context;
using malone.Core.Identity.EntityFramework.DAL.Mappings;
using malone.Core.Identity.EntityFramework.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace malone.Core.Identity.EntityFramework.Context
{
    public class EFIdentityDbContext<TKey, TUserEntity, TRoleEntity, TUserLogin, TUserRole, TUserClaim> 
        : IdentityDbContext<TUserEntity, TRoleEntity, TKey, TUserLogin, TUserRole, TUserClaim>, IEFContext
        where TKey : IEquatable<TKey>
        where TUserClaim : CoreUserClaim<TKey>
        where TUserRole : CoreUserRole<TKey>
        where TUserLogin : CoreUserLogin<TKey>
        where TRoleEntity : CoreRole<TKey, TUserRole>
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
    {
        public EFIdentityDbContext() : base() { }
        public EFIdentityDbContext(string nameOrConnectionStringName) : base(nameOrConnectionStringName)
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

    public class EFIdentityDbContext : EFIdentityDbContext<int, CoreUser, CoreRole, CoreUserLogin, CoreUserRole, CoreUserClaim>
    {
        public EFIdentityDbContext() : base() { }

        public EFIdentityDbContext(string nameOrConnectionStringName)
            : base(nameOrConnectionStringName)
        {

        }

    }

}
