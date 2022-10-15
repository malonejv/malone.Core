using malone.Core.Commons.DI;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Identity.EntityFramework;
using malone.Core.Identity.EntityFramework.Context;
using malone.Core.Identity.EntityFramework.Entities;
using malone.Core.IoC;
using malone.Core.Sample.EF.SqlServer.Middle.DAL.Context.Conventions;
using malone.Core.Sample.EF.SqlServer.Middle.DAL.Context.Mappings;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace malone.Core.Sample.EF.SqlServer.Middle.DAL.Context
{
    public class SampleContext : CoreIdentityDbContext //CoreDbContext
    {
        public SampleContext() : base() { }

        public SampleContext(string connectionStringName)
            : base(connectionStringName)
        {
            //TODO: Habilitar configuración
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            //OPTION: Habilita la clase SampleContextInitializer 
            //Database.SetInitializer<SampleContext>(null);

            //En lugar de habilitar o deshabilitar por codigo tambien se puede hacer por web.config con la siguiente configuracion
            //<entityFramework>
            //<contexts>
            //  <context type="malone.Core.Sample.EF.SqlServer.Middle.DAL.Context.EF.SampleContext, malone.Core.Sample.EF.SqlServer.Middle">
            //    <databaseInitializer type="malone.Core.Sample.EF.SqlServer.Middle.DAL.Context.EF.SampleContextInitializer, malone.Core.Sample.EF.SqlServer.Middle" />
            //  </context>
            //</contexts>
            //</entityFramework>

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new ForeignKeyNamingConvention());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new TodoListMapping());
            modelBuilder.Configurations.Add(new TaskItemMapping());
        }

        public System.Data.Entity.DbSet<malone.Core.Sample.EF.SqlServer.Middle.EL.Model.TodoList> TodoLists { get; set; }
    }

    public class SampleContextInitializer : CreateDatabaseIfNotExists<SampleContext>
    {
        public SampleContextInitializer() : base()
        {
        }

        protected override void Seed(SampleContext context)
        {
            try
            {
                var userManager = ServiceLocator.Current.Get<UserService>();
                var AdminstradorDesc = RoleType.Administrador.GetDescription();
                var UsuarioDesc = RoleType.Usuario.GetDescription();

                var existeRol = context.Set<CoreRole>().Where(r => r.Name == AdminstradorDesc).Any();
                if (!existeRol)
                {
                    // USUARIO Y ROL DE ADMINISTRADOR POR DEFECTO
                    // RoleTypes is a class containing constant string values for different roles
                    List<CoreRole> roles = new List<CoreRole>();
                    roles.Add(new CoreRole() { Name = AdminstradorDesc });
                    roles.Add(new CoreRole() { Name = UsuarioDesc });

                    context.Set<CoreRole>().AddOrUpdate(roles.ToArray());
                    context.SaveChanges();
                }

                var existeAdmin = context.Set<CoreUser>().Where(u => u.UserName == "admin").Any();

                if (!existeAdmin)
                {
                    // Initialize default user
                    CoreUser admin = new CoreUser();
                    admin.Email = "admin@sample.com";
                    admin.UserName = "admin";
                    admin.EmailConfirmed = true;

                    var adminRole = context.Set<CoreRole>().Where(r => r.Name == AdminstradorDesc).FirstOrDefault();

                    admin.Roles.Add(new CoreUserRole()
                    {
                        RoleId = adminRole.Id
                    });

                    userManager.Create(admin, "admin.1234");

                    context.SaveChanges();

                }

                var existeUser = context.Set<CoreUser>().Where(u => u.UserName == "user").Any();

                if (!existeUser)
                {
                    // Initialize default user
                    CoreUser user = new CoreUser();
                    user.Email = "user@sample.com";
                    user.UserName = "user";
                    user.EmailConfirmed = true;

                    var usuarioRole = context.Set<CoreRole>().Where(r => r.Name == UsuarioDesc).FirstOrDefault();

                    user.Roles.Add(new CoreUserRole()
                    {
                        RoleId = usuarioRole.Id
                    });

                    userManager.Create(user, "user.1234");

                    context.SaveChanges();
                }

                //--------------------------------------------------------------------
                //TEST LIST & ITEMS

                var sampleUser = context.Set<CoreUser>().Where(u => u.UserName == "user").FirstOrDefault();
                var existeListaEjemplo = context.Set<TodoList>().Where(r => r.Name == "Sample List").Any();

                if (!existeListaEjemplo)
                {
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
                        IsDeleted = false,
                        User = sampleUser
                    };
                    context.Set<TodoList>().AddOrUpdate(list);
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
