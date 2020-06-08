using malone.Core.Commons.Configurations;
using malone.Core.Commons.Exceptions.Handler;
using malone.Core.Commons.Exceptions.Manager;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Initializers;
using malone.Core.DataAccess.UnitOfWork;
using malone.Core.EF.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            container.RegisterType<IEFExceptionHandler, EFExceptionHandler>();
            container.RegisterType<IEFMessageHandler, EFMessageHandler>();
        }
    }
}
