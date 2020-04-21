using malone.Core.AdoNet.DAL.Database;
using malone.Core.CL.Configurations.Sections.Feature;
using malone.Core.DAL.Context;
using malone.Core.DAL.Repositories;
using malone.Core.DAL.UnitOfWork;
using malone.Core.EF.DAL.Repositories.Implementations;
using malone.Core.Sample.Middle.CL.Features;
using malone.Core.Sample.Middle.DAL.Context.AdoNet;
using malone.Core.Sample.Middle.DAL.Context.EF;
using malone.Core.Sample.Middle.DAL.Repositories.AdoNet;
using malone.Core.Sample.Middle.EL.Model;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace malone.core.Sample.DI
{
    public static class RegisterDataAccessLayerTypes
    {
        public static IUnityContainer RegisterTypes(IUnityContainer container)
        {
            #region EF

            //Context
            container.RegisterType<IContext, SampleEFContext>(new PerRequestLifetimeManager(), new InjectionConstructor("SampleConnection"));

            //Repositories
            container.RegisterType<IRepository<TodoList>, EFRepository<TodoList>>();
            container.RegisterType<IRepository<TaskItem>, EFRepository<TaskItem>>();

            #endregion

            //General
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());

            //container.RegisterType<IUserStore<User,string>, UserStore<User,Role,string,UserLogin,UserRole,UserClaim>>();

            return container;
        }
    }
}
