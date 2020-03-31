using malone.Core.CL.Configurations.Sections;
using malone.Core.DAL.AdoNet.Factory;
using malone.Core.DAL.AdoNet.Repositories.Implementations;
using malone.Core.DAL.Base.Context;
using malone.Core.DAL.Base.Repositories;
using malone.Core.DAL.Base.UnitOfWork;
using malone.Core.DAL.EF.Repositories.Implementations;
using malone.Core.Sample.Middle.CL.Features;
using malone.Core.Sample.Middle.DAL.Context.AdoNet;
using malone.Core.Sample.Middle.DAL.Context.EF;
using malone.Core.Sample.Middle.DAL.Repositories.AdoNet;
using malone.Core.Sample.Middle.EL;
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
                container.RegisterType<IRepository<TodoList>, EFRepository<TodoList>>();
                container.RegisterType<IRepository<TaskItem>, EFRepository<TaskItem>>();

                #endregion
            }
            else if(FeatureSettings.IsEnabled(Features.AdoNet))
            {
                #region AdoNet

                //Context
                container.RegisterInstance(new DatabaseFactory(Configurations.ConnectionStringName));
                container.RegisterType<IContext, SampleAdoNetContext>(new PerRequestLifetimeManager(), new InjectionConstructor(container.Resolve<DatabaseFactory>()));

                //Repositories
                container.RegisterType<IRepository<TodoList>, ANTodoListRepository>();
                container.RegisterType<IRepository<TaskItem>, ANTaskItemRepository>();

                #endregion
            }

            //General
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());

            //container.RegisterType<IUserStore<User,string>, UserStore<User,Role,string,UserLogin,UserRole,UserClaim>>();

            return container;
        }
    }
}
