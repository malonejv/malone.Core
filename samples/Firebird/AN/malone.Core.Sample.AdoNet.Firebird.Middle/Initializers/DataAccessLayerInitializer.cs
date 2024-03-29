﻿using malone.Core.Commons.DI;
using malone.Core.Commons.Initializers;
using malone.Core.DataAccess.Context;
using malone.Core.DataAccess.Repositories;
using malone.Core.Sample.AdoNet.Firebird.Middle.DAL.Context;
using malone.Core.Sample.AdoNet.Firebird.Middle.DAL.Repositories;
using malone.Core.Sample.AdoNet.Firebird.Middle.EL.Model;
using Unity;
using Unity.Injection;

namespace malone.Core.Sample.AdoNet.Firebird.Middle.Initializers
{
    public class DataAccessLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //Context
            container.RegisterType<IContext, SampleContext>(new PerRequestLifetimeManager(), new InjectionConstructor("SampleConnection"));

            //Agrego esta configuración por la siguiente configuracion: container.RegisterType<IRoleStore<CoreRole, int>, RoleRepository<AdoNetIdentityDbContext>>();
            var context = ServiceLocator.Current.Get<IContext>();
            container.RegisterInstance(context as DbContext);

            //Repositories
            container.RegisterType<IRepository<TodoList>, TodoListRepository>();
            container.RegisterType<IRepository<TaskItem>, Repository<TaskItem>>();

        }
    }
}
