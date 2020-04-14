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
            if (FeatureSettings.IsEnabled(Features.EF))
            {
                #region EF

                //Context
                container.RegisterType<IContext, SampleEFContext>(new PerRequestLifetimeManager(), new InjectionConstructor("SampleConnection"));

                //Repositories
                container.RegisterType<IRepository<decimal, TodoList>, EFRepository<decimal, TodoList>>();
                container.RegisterType<IRepository<decimal, TaskItem>, EFRepository<decimal, TaskItem>>();

                #endregion
            }
            else if (FeatureSettings.IsEnabled(Features.AdoNet))
            {
                #region AdoNet

                //Context
                container.RegisterInstance(new DatabaseFactory());
                container.RegisterType<IContext, SampleAdoNetContext>(new PerRequestLifetimeManager(), new InjectionConstructor(container.Resolve<DatabaseFactory>()));

                //Repositories
                container.RegisterType<IRepository<decimal, TodoList>, ANTodoListRepository>();
                container.RegisterType<IRepository<decimal, TaskItem>, ANTaskItemRepository>();

                #endregion
            }

            //General
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());

            //container.RegisterType<IUserStore<User,string>, UserStore<User,Role,string,UserLogin,UserRole,UserClaim>>();

            return container;
        }
    }
}
