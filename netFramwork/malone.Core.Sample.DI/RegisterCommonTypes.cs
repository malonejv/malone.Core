using log4net;
using malone.Core.CL.Configurations;
using malone.Core.CL.Configurations.CoreConfiguration;
using malone.Core.CL.Exceptions.Handler.Implementations;
using malone.Core.CL.Exceptions.Handler.Interfaces;
using malone.Core.CL.Exceptions.Manager.Implementations;
using malone.Core.CL.Exceptions.Manager.Interfaces;
using malone.Core.CL.Log;
using malone.Core.CL.Log.Log4Net;
using malone.Core.CL.Logging.Log4Net;
using Unity;
using Unity.Injection;

namespace malone.core.Sample.DI
{
    public static class RegisterCommonTypes
    {
        public static IUnityContainer RegisterTypes(IUnityContainer container)
        {

            container.RegisterType<ICoreConfiguration, CoreConfiguration>();

            ILog logger = LogManager.GetLogger("SampleLogger");
            //container.RegisterInstance<ILog>(logger);

            container.RegisterType<LoggerFactory, Log4NetLoggerFactory>();
            container.RegisterType<ILogger, Log4netLogger>(new InjectionConstructor(logger));

            container.RegisterType<IExceptionMessageManager, ExceptionMessageManager>();
            container.RegisterType<IExceptionHandler, ExceptionHandler>();

            return container;
        }
    }
}
