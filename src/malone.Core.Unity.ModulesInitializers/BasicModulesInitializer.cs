﻿using malone.Core.Commons.Configurations;
using malone.Core.Commons.Exceptions.Handler;
using malone.Core.Commons.Exceptions.Manager;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Initializers;
using malone.Core.DataAccess.UnitOfWork;
using Unity;
using Unity.Lifetime;

namespace malone.Core.Unity.ModulesInitializers
{
    public class BasicModulesInitializer : IModuleInitializer<IUnityContainer>
    {
        public string Name => CoreModules.Basics.GetDescription();

        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<ICoreConfiguration, CoreConfiguration>();
            container.RegisterType<IUnitOfWork, UnitOfWork>(new SingletonLifetimeManager());
            container.RegisterType<ICoreExceptionHandler, CoreExceptionHandler>();
            container.RegisterType<ICoreMessageHandler, CoreMessageHandler>();
        }
    }
}