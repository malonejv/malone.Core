﻿using malone.Core.Commons.Initializers;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using Unity;

namespace malone.Core.Sample.EF.SqlServer.Middle.Initializers
{
    public class EntitiesLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<TodoList>();
            container.RegisterType<TaskItem>();
        }
    }
}
