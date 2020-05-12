using malone.Core.Identity.EntityFramework.EL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Identity.EntityFramework.DAL.Mappings
{
    public class CoreUserMapping<TKey, TUserEntity, TUserLogin, TUserRole, TUserClaim> : EntityTypeConfiguration<TUserEntity>
        where TKey : IEquatable<TKey>
        where TUserClaim : CoreUserClaim<TKey>
        where TUserRole : CoreUserRole<TKey>
        where TUserLogin : CoreUserLogin<TKey>
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
    {
        public CoreUserMapping()
        {
            EntityTypeConfiguration<TUserEntity> entityUserConfiguration = ToTable("Users");
            entityUserConfiguration.HasMany<TUserRole>((TUserEntity u) => (ICollection<TUserRole>)u.Roles).WithOptional().HasForeignKey<TKey>((TUserRole ur) => ur.UserId);
            entityUserConfiguration.HasMany<TUserClaim>((TUserEntity u) => (ICollection<TUserClaim>)u.Claims).WithOptional().HasForeignKey<TKey>((TUserClaim uc) => uc.UserId);
            entityUserConfiguration.HasMany<TUserLogin>((TUserEntity u) => (ICollection<TUserLogin>)u.Logins).WithRequired().HasForeignKey<TKey>((TUserLogin ul) => ul.UserId);
            StringPropertyConfiguration indexUserName = entityUserConfiguration.Property((TUserEntity u) => u.UserName).IsRequired().HasMaxLength(new int?(256));
            string userNameIndexColumnName = "Index";
            IndexAttribute indexUserNameAttribute = new IndexAttribute("UserNameIndex");
            indexUserNameAttribute.IsUnique = true;
            indexUserName.HasColumnAnnotation(userNameIndexColumnName, new IndexAnnotation(indexUserNameAttribute));
            entityUserConfiguration.Property((TUserEntity u) => u.Email).HasMaxLength(new int?(256));

        }
    }
}
