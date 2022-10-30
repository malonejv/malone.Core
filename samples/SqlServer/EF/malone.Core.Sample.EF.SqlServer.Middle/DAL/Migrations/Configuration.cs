using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Initializers;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.EF.Repositories.Implementations;
using malone.Core.Identity.EntityFramework;
using malone.Core.Identity.EntityFramework.Entities;
using malone.Core.IoC;
using malone.Core.Sample.EF.SqlServer.Middle.DAL.Context;
using malone.Core.Sample.EF.SqlServer.Middle.DAL.Repositories;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using malone.Core.Sample.EF.SqlServer.Middle.Initializers;
using malone.Core.Unity;
using malone.Core.Unity.IdentityEntityFrameworkInitializer;
using malone.Core.Unity.Log4NetInitializer;
using Microsoft.AspNet.Identity;
using Unity;
using Unity.Injection;


namespace malone.Core.Sample.EF.SqlServer.Middle.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SampleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\Migrations";
        }

        protected override void Seed(SampleContext context)
        {

            if (!System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Launch();

            try
            {
                //Agrego estas líneas porque no me está cargando los assemblies dinámicamente cuando ejecuto el update-database
                _ = ServiceLocator.Current.Get<Log4NetModuleInitializer>();
                _ = ServiceLocator.Current.Get<IdentityEntityFrameworkModuleInitializer>();

                AppInitializer<UnityActivator, IUnityContainer, ConfigurationInitializer>.Initialize();


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
                    TodoList list = new TodoList(
                        "Sample List",
                        sampleUser,
                        new List<TaskItem>()
                        {
                            new TaskItem("Sample Item 1"),
                            new TaskItem("Sample Item 2")
                        });
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

    public class ConfigurationInitializer : LayersInitializer<IUnityContainer>
    {
        public ConfigurationInitializer()
        {
        }

        public override IEnumerable<IInitializer<IUnityContainer>> Layers
        {
            get
            {
                return new IInitializer<IUnityContainer>[]
              {
                    new CommonLayerInitializer(),
                    new EntitiesLayerInitializer(),
                    new DataAccessConfigurationInitializer()
              };
            }
        }
    }

    public class DataAccessConfigurationInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //Context
            container.RegisterType<IContext, SampleContext>(new InjectionConstructor("SampleConnection"));

            //Agrego esta configuración por la siguiente configuracion: container.RegisterType<IRoleStore<CoreRole, int>, RoleRepository<EFIdentityDbContext>>();
            var context = ServiceLocator.Current.Get<IContext>();
            container.RegisterInstance(context as DbContext);

            //Repositories
            container.RegisterType<IRepository<TodoList>, TodoListRepository>();
            container.RegisterType<IRepository<TaskItem>, Repository<TaskItem>>();

            container.RegisterInstance(UserService.CreateAndConfigure(new Microsoft.AspNet.Identity.Owin.IdentityFactoryOptions<UserService>(), null));
        }
    }
}
