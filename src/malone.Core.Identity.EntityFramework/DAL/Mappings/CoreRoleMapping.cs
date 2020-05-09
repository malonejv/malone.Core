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
    public class CoreRoleMapping<TRoleEntity, TUserRole, TKey> : EntityTypeConfiguration<TRoleEntity>
        where TKey : IEquatable<TKey>
        where TUserRole : CoreUserRole<TKey>
        where TRoleEntity : CoreRole<TKey, TUserRole>
    {
        public CoreRoleMapping()
        {
            EntityTypeConfiguration<TRoleEntity> entityRoleConfiguration = ToTable("Roles");
            StringPropertyConfiguration indexRoleName = entityRoleConfiguration.Property((TRoleEntity r) => r.Name).IsRequired().HasMaxLength(new int?(256));
            string roleNameIndexColumnName = "Index";
            IndexAttribute indexRolaNameAttribute = new IndexAttribute("RoleNameIndex");
            indexRolaNameAttribute.IsUnique = true;
            indexRoleName.HasColumnAnnotation(roleNameIndexColumnName, new IndexAnnotation(indexRolaNameAttribute));

            entityRoleConfiguration.HasMany<TUserRole>((TRoleEntity r) => (ICollection<TUserRole>)r.Users).WithRequired().HasForeignKey<TKey>((TUserRole ur) => ur.RoleId);

        }


    }
}
