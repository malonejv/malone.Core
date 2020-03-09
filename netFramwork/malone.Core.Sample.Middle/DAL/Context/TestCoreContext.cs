using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.DAL.EF.Context;
using malone.Core.Sample.Middle.DAL.Mappings;

namespace malone.Core.Sample.Middle.DAL.Context
{
    public class SampleContext : EFDbContext
    {
        public SampleContext(string connectionStringName)
            : base(connectionStringName)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            //Habilita la clase MIPContextInitializer
            Database.SetInitializer<SampleContext>(new SampleContextInitializer());
            //Database.SetInitializer<SampleContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            registerUserIdentityMapping(modelBuilder);
            modelBuilder.Configurations.Add(new TodoListMapping());
            modelBuilder.Configurations.Add(new TaskItemMapping());
        }



        private void registerUserIdentityMapping(DbModelBuilder modelBuilder)
        {
            //EntityTypeConfiguration<Role> entityRoleConfiguration = modelBuilder.Entity<Role>();
            //entityRoleConfiguration.ToTable("Roles");
            //StringPropertyConfiguration indexRoleName = entityRoleConfiguration.Property((Role r) => r.Name).IsRequired().HasMaxLength(new int?(256));
            //string roleNameIndexColumnName = "Index";
            //IndexAttribute indexRolaNameAttribute = new IndexAttribute("RoleNameIndex");
            //indexRolaNameAttribute.IsUnique = true;
            //indexRoleName.HasColumnAnnotation(roleNameIndexColumnName, new IndexAnnotation(indexRolaNameAttribute));
            //entityRoleConfiguration.HasMany<UserRole>((Role r) => (ICollection<UserRole>)r.Users).WithRequired().HasForeignKey<string>((UserRole ur) => ur.RoleId);

            //EntityTypeConfiguration<User> entityUserConfiguration = modelBuilder.Entity<User>();
            //entityUserConfiguration.ToTable("Users");
            //entityUserConfiguration.Property(p => p.Id);
            //entityUserConfiguration.HasMany<UserRole>((User u) => (ICollection<UserRole>)u.Roles).WithOptional().HasForeignKey<string>((UserRole ur) => ur.UserId);
            //entityUserConfiguration.HasMany<UserClaim>((User u) => (ICollection<UserClaim>)u.Claims).WithOptional().HasForeignKey<string>((UserClaim uc) => uc.UserId);
            //entityUserConfiguration.HasMany<UserLogin>((User u) => (ICollection<UserLogin>)u.Logins).WithRequired().HasForeignKey<string>((UserLogin ul) => ul.UserId);
            //StringPropertyConfiguration indexUserName = entityUserConfiguration.Property((User u) => u.UserName).IsRequired().HasMaxLength(new int?(256));
            //string userNameIndexColumnName = "Index";
            //IndexAttribute indexUserNameAttribute = new IndexAttribute("UserNameIndex");
            //indexUserNameAttribute.IsUnique = true;
            //indexUserName.HasColumnAnnotation(userNameIndexColumnName, new IndexAnnotation(indexUserNameAttribute));
            //entityUserConfiguration.Property((User u) => u.Email).HasMaxLength(new int?(256));

            //modelBuilder.Entity<UserRole>().ToTable("UserRoles")
            //    .HasKey((UserRole r) => new
            //    {
            //        r.UserId,
            //        r.RoleId
            //    });

            //modelBuilder.Entity<UserLogin>().ToTable("UserLogins")
            //    .HasKey((UserLogin l) => new
            //    {
            //        l.LoginProvider,
            //        l.ProviderKey,
            //        l.UserId
            //    });

            //modelBuilder.Entity<UserClaim>().ToTable("UserClaims");

        }
    }

    public class SampleContextFactory : IDbContextFactory<SampleContext>
    {
        public SampleContext Create()
        {
            var context = new SampleContext("SampleConnection");
            return context;
        }
    }

    public class SampleContextInitializer : DropCreateDatabaseIfModelChanges<SampleContext>
    {
        protected override void Seed(SampleContext dbContext)
        {
            //// USUARIO Y ROL DE ADMINISTRADOR POR DEFECTO
            //var roleStore = new RoleStore<Role, string, UserRole>(dbContext);
            //var roleManager = new RoleManager<Role>(roleStore);
            //// RoleTypes is a class containing constant string values for different roles
            //List<Role> roles = new List<Role>();
            //roles.Add(new Role() { Name = Rol.Administrador.GetStringValue() });
            //roles.Add(new Role() { Name = Rol.Administrativo.GetStringValue() });
            //roles.Add(new Role() { Name = Rol.Empleado.GetStringValue() });
            //roles.Add(new Role() { Name = Rol.Usuario.GetStringValue() });

            //foreach (Role role in roles)
            //{
            //    roleManager.Create(role);
            //}

            //// Initialize default user
            //var userStore = new UserStore<User, Role, string, UserLogin, UserRole, UserClaim>(dbContext);
            //var userManager = new UserManager<User, string>(userStore);
            //User admin = new User();
            //admin.Email = "malonejv@gmail.com";
            //admin.UserName = "admin";

            //userManager.Create(admin, "TLC2018!");
            //userManager.AddToRole(admin.Id, Rol.Administrador.GetStringValue());

            ////--------------------------------------------------------------------

            ////SERVICIOS
            //var uow = new UnitOfWork(dbContext, null);
            //var servicioRepository = new ServicioRepository(uow);

            //servicioRepository.Insert(new Servicio()
            //{
            //    Descripcion = "Aire Acondicionado",
            //    NombreArchivo = "aa-icon.png"
            //});

            //servicioRepository.Insert(new Servicio()
            //{
            //    Descripcion = "Gimnasio",
            //    NombreArchivo = "dumbbell-icon.png"
            //});

            //servicioRepository.Insert(new Servicio()
            //{
            //    Descripcion = "Wifi gratis",
            //    NombreArchivo = "wifi-icon.png"
            //});

            //dbContext.SaveChanges();
        }
    }
}
