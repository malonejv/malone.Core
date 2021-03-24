using malone.Core.Commons.DI;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Initializers;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.EF.Repositories.Implementations;
using malone.Core.Identity.EntityFramework;
using malone.Core.Identity.EntityFramework.Entities;
using malone.Core.Sample.EF.Firebird.Middle.DAL.Context;
using malone.Core.Sample.EF.Firebird.Middle.DAL.Repositories;
using malone.Core.Sample.EF.Firebird.Middle.EL.Model;
using malone.Core.Sample.EF.Firebird.Middle.Initializers;
using malone.Core.Unity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using Unity;
using Unity.Injection;

namespace malone.Core.Sample.EF.Firebird.Middle.DAL.Migrations
{
    /// <summary>
    /// Se ejecuta cuando se ejecuta el comando update-database
    /// </summary>
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
                AppInitializer<UnityActivator, IUnityContainer, ConfigurationInitializer>.Initialize();

                var userManager = ServiceLocator.Current.Get<UserBusinessComponent>();
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

            container.RegisterInstance(UserBusinessComponent.CreateAndConfigure(new Microsoft.AspNet.Identity.Owin.IdentityFactoryOptions<UserBusinessComponent>(), null));
        }
    }
}
