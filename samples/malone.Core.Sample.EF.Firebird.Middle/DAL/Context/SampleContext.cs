using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using FirebirdSql.Data.FirebirdClient;
using malone.Core.Commons.Configurations;
using malone.Core.Commons.DI;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Identity.EntityFramework.Context;
using malone.Core.Identity.EntityFramework.Entities;
using malone.Core.Sample.EF.Firebird.Middle.DAL.Context.Mappings;
using malone.Core.Sample.EF.Firebird.Middle.EL.Model;
using Microsoft.AspNet.Identity;

namespace malone.Core.Sample.EF.Firebird.Middle.DAL.Context
{
    public class SampleContext : CoreIdentityDbContext //EFDbContext
    {
        public SampleContext() : this("SampleConnection") { }

        public SampleContext(string connectionStringName)
            : base(connectionStringName)
        {
            //TODO: Habilitar configuración
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            //OPTION: Habilita la clase SampleContextInitializer 
            Database.SetInitializer<SampleContext>(null);

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new TodoListMapping());
            modelBuilder.Configurations.Add(new TaskItemMapping());
        }

        public System.Data.Entity.DbSet<malone.Core.Sample.EF.Firebird.Middle.EL.Model.TodoList> TodoLists { get; set; }
    }
    public class SampleContextDbConnectionFactory : IDbConnectionFactory
    {
        public SampleContextDbConnectionFactory()
        {
        }

        #region IDbConnectionFactory implementation
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            var configuration = ServiceLocator.Current.Get<ICoreConfiguration>();
            var connectionString = configuration.GetConnectionString("SampleConnection");
            var connection = new FbConnection(connectionString);
            return connection;
        }
        #endregion
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
        public SampleContextInitializer() : base()
        {
        }

        protected override void Seed(SampleContext dbContext)
        {
            // USUARIO Y ROL DE ADMINISTRADOR POR DEFECTO
            // RoleTypes is a class containing constant string values for different roles
            List<CoreRole> roles = new List<CoreRole>();
            roles.Add(new CoreRole() { Name = RoleType.Administrador.GetDescription() });
            roles.Add(new CoreRole() { Name = RoleType.Administrativo.GetDescription() });
            roles.Add(new CoreRole() { Name = RoleType.Empleado.GetDescription() });
            roles.Add(new CoreRole() { Name = RoleType.Usuario.GetDescription() });

            dbContext.Set<CoreRole>().AddOrUpdate(roles.ToArray());

            // Initialize default user
            CoreUser admin = new CoreUser();
            admin.Email = "malonejv@gmail.com";
            admin.UserName = "admin";

            //TODO: Usar Secrets para obtener el password de admin
            PasswordHasher hasher = new PasswordHasher();
            admin.PasswordHash = hasher.HashPassword("Adm1n.M4l0ne");

            dbContext.Set<CoreUser>().AddOrUpdate(admin);

            //--------------------------------------------------------------------
            //TEST LIST & ITEMS

            TodoList list = new TodoList()
            {
                Name = "Sample List",
                Items = new List<TaskItem>()
                {
                    new TaskItem()
                    {
                        Description="Sample Item 1",
                        IsDeleted = false
                    },

                    new TaskItem()
                    {
                        Description="Sample Item 2",
                        IsDeleted = false
                    }
                },
                IsDeleted = false
            };
            dbContext.Set<TodoList>().AddOrUpdate(list);

            dbContext.SaveChanges();
        }
    }
}
