using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using GMS.Core.DAL.Context.EF.Mappings;
using malone.Core.Commons.DI;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.DataAccess.Context;
using malone.Core.EF.Context;

namespace GMS.Core.DAL.Context.EF
{
    public class GMSContext : EFDbContext //EFDbContext
    {
        public static GMSContext Instance { get; private set; }

        public GMSContext(string connectionStringName)
            : base(connectionStringName)
        {
            //TODO: Habilitar configuración
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            //OPTION: Habilita la clase SampleContextInitializer 
            Database.SetInitializer<GMSContext>(null);

            Instance = this;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new TodoListMapping());
            modelBuilder.Configurations.Add(new TaskItemMapping());
        }


        public static GMSContext Create()
        {
            if (Instance == null)
                Instance = ServiceLocator.Current.Get<IContext>() as GMSContext;

            return Instance;
        }

        public System.Data.Entity.DbSet<GMS.Core.EL.Model.TodoList> TodoLists { get; set; }
    }

    public class SampleContextFactory : IDbContextFactory<GMSContext>
    {
        public GMSContext Create()
        {
            var context = new GMSContext("GMSConnection");
            return context;
        }
    }

    public class SampleContextInitializer : DropCreateDatabaseIfModelChanges<GMSContext>
    {
        public SampleContextInitializer() : base()
        {
        }

        protected override void Seed(GMSContext dbContext)
        {
            //// USUARIO Y ROL DE ADMINISTRADOR POR DEFECTO
            //// RoleTypes is a class containing constant string values for different roles
            //List<CoreRole> roles = new List<CoreRole>();
            //roles.Add(new CoreRole() { Name = RoleType.Administrador.GetDescription() });
            //roles.Add(new CoreRole() { Name = RoleType.Administrativo.GetDescription() });
            //roles.Add(new CoreRole() { Name = RoleType.Empleado.GetDescription() });
            //roles.Add(new CoreRole() { Name = RoleType.Usuario.GetDescription() });

            //dbContext.Set<CoreRole>().AddOrUpdate(roles.ToArray());

            //// Initialize default user
            //CoreUser admin = new CoreUser();
            //admin.Email = "malonejv@gmail.com";
            //admin.UserName = "admin";

            ////TODO: Usar Secrets para obtener el password de admin
            //PasswordHasher hasher = new PasswordHasher();
            //admin.PasswordHash = hasher.HashPassword("Adm1n.M4l0ne");

            //dbContext.Set<CoreUser>().AddOrUpdate(admin);

            ////--------------------------------------------------------------------
            ////TEST LIST & ITEMS

            //TodoList list = new TodoList()
            //{
            //    Name = "Sample List",
            //    Items = new List<TaskItem>()
            //    {
            //        new TaskItem()
            //        {
            //            Description="Sample Item 1",
            //            IsDeleted = false
            //        },

            //        new TaskItem()
            //        {
            //            Description="Sample Item 2",
            //            IsDeleted = false
            //        }
            //    },
            //    IsDeleted = false
            //};
            //dbContext.Set<TodoList>().AddOrUpdate(list);

            //dbContext.SaveChanges();
        }
    }
}
