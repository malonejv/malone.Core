using log4net;
using malone.Core.Commons.Configurations;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Initializers;
using malone.Core.Commons.Log;
using malone.Core.Log4Net;
using Unity;
using Unity.Injection;

namespace malone.Core.Unity.Log4NetInitializer
{
    public class Log4NetModuleInitializer : IModuleInitializer<IUnityContainer>
    {
        public string Name => CoreModules.Log4NetLogger.GetDescription();

        public void Initialize(IUnityContainer container)
        {
            ILog logger = LogManager.GetLogger("CoreLogger");
            //container.RegisterInstance<ILog>(logger);

            container.RegisterType<LoggerFactory, Log4NetLoggerFactory>();
            container.RegisterType<ILogger, Log4netLogger>(new InjectionConstructor(logger));

        }
    }
}
