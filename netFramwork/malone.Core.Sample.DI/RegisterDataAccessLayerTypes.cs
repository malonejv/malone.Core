using Unity;
using Unity.AspNet.Mvc;
using malone.Core.Sample.Middle.EL;
using malone.Core.DAL.EF.Repositories;
using malone.Core.DAL.Base.Repositories;
using malone.Core.DAL.Base.UnitOfWork;
using malone.Core.DAL.Base.Context;
using malone.Core.Sample.Middle.DAL.Context;
using Unity.Injection;

namespace malone.core.Sample.DI
{
    public static class RegisterDataAccessLayerTypes
    {
        public static IUnityContainer RegisterTypes(IUnityContainer container)
        {
            //EF Code First
            container.RegisterType<IContext, SampleContext>(new PerRequestLifetimeManager(), new InjectionConstructor("SampleConnection"));
            container.RegisterType<IRepository<TodoList>, EFRepository<TodoList>>();
            container.RegisterType<IRepository<TaskItem>, EFRepository<TaskItem>>();
            
            //General
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());

            //container.RegisterType<IUserStore<User,string>, UserStore<User,Role,string,UserLogin,UserRole,UserClaim>>();
            
            return container;
        }
    }
}
