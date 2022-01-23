﻿using malone.Core.Business.Components;
using malone.Core.Commons.Initializers;
using malone.Core.Sample.AdoNet.SqlServer.Middle.BL;
using malone.Core.Sample.AdoNet.SqlServer.Middle.BL.Implementations;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;
using Unity;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.Initializers
{
    public class BusinessLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //BUSINESS VALIDATORS
            container.RegisterType<ITodoListBV, TodoListBV>();
            container.RegisterType<IBusinessValidator<TaskItem>, BusinessValidator<TaskItem>>();

            //BUSINESS COMPONENTS
            container.RegisterType<ITodoListBC, TodoListBC>();
            container.RegisterType<ITaskItemBC, TaskItemBC>();
        }
    }
}