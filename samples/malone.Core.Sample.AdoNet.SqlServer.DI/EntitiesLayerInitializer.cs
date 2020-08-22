﻿using malone.Core.Commons.Initializers;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;
using Unity;

namespace malone.Core.Sample.AdoNet.SqlServer.DI
{
    public class EntitiesLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //container.RegisterType<CoreUser>();
            //container.RegisterType<CoreRole>();
            //container.RegisterType<CoreUserLogin>();
            //container.RegisterType<CoreUserRole>();
            //container.RegisterType<CoreUserClaim>();

            container.RegisterType<TodoList>();
            container.RegisterType<TaskItem>();

        }
    }
}
