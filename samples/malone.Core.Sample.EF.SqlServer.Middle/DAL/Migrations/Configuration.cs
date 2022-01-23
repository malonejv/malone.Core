namespace malone.Core.Sample.EF.SqlServer.Middle.DAL.Migrations
{
    using malone.Core.Commons.Helpers.Extensions;
    using malone.Core.Identity.EntityFramework.Entities;
    using malone.Core.Sample.EF.SqlServer.Middle.DAL.Context.EF;
    using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>
    /// Se ejecuta cuando se ejecuta el comando update-database
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<SampleEFContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\Migrations";
        }

        protected override void Seed(SampleEFContext context)
        {
            try
            {
                var existeRol = context.Set<CoreRole>().Where(r => r.Name == RoleType.Administrador.GetDescription()).Any();
                if (!existeRol)
                {
                    // USUARIO Y ROL DE ADMINISTRADOR POR DEFECTO
                    // RoleTypes is a class containing constant string values for different roles
                    List<CoreRole> roles = new List<CoreRole>();
                    roles.Add(new CoreRole() { Name = RoleType.Administrador.GetDescription() });
                    roles.Add(new CoreRole() { Name = RoleType.Administrativo.GetDescription() });
                    roles.Add(new CoreRole() { Name = RoleType.Empleado.GetDescription() });
                    roles.Add(new CoreRole() { Name = RoleType.Usuario.GetDescription() });

                    context.Set<CoreRole>().AddOrUpdate(roles.ToArray());
                    context.SaveChanges();
                }
            }
            catch (Exception) { }

            try
            {
                var existeAdmin = context.Set<CoreUser>().Where(u => u.UserName == "admin").Any();

                if (!existeAdmin)
                {
                    // Initialize default user
                    CoreUser admin = new CoreUser();
                    admin.Email = "malonejv@gmail.com";
                    admin.UserName = "admin";

                    //TODO: Usar Secrets para obtener el password de admin
                    PasswordHasher hasher = new PasswordHasher();
                    admin.PasswordHash = hasher.HashPassword("Adm1n.M4l0ne");

                    context.Set<CoreUser>().AddOrUpdate(admin);
                    context.SaveChanges();
                }
            }
            catch (Exception) { }


            //--------------------------------------------------------------------
            //TEST LIST & ITEMS
            try
            {
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
                        IsDeleted = false
                    };
                    context.Set<TodoList>().AddOrUpdate(list);
                    context.SaveChanges();
                }
            }
            catch (Exception) { }

        }
    }
}
