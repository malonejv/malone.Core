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
            container.RegisterType<LoggerFactory, Log4NetLoggerFactory>();

            LoggerFactory loggerFactory = container.Resolve<LoggerFactory>();

            ILogger logger = loggerFactory.GetLogger();
            container.RegisterInstance(logger);
        }
    }
}
