using malone.Core.Identity.EntityFramework.EL;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
