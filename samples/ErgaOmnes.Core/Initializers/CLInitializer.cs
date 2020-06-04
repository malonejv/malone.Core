using ErgaOmnes.Core.CL.Exceptions;
using log4net;
using malone.Core.CL.Configurations;
using malone.Core.CL.Configurations.Features;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.CL.Exceptions.Manager;
using malone.Core.CL.Initializers;
using malone.Core.CL.Log;
using malone.Core.CL.Log.Log4Net;
using malone.Core.CL.Logging.Log4Net;
using Unity;
using Unity.Injection;

namespace ErgaOmnes.Core.Initializers
{
    public class CLInitializer : ILayer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            ILog logger = LogManager.GetLogger("SampleLogger");
            //container.RegisterInstance<ILog>(logger);

            container.RegisterType<LoggerFactory, Log4NetLoggerFactory>();
            container.RegisterType<ILogger, Log4netLogger>(new InjectionConstructor(logger));

            container.RegisterType<IMessageHandler<ErrorCodes>, MessageHandler<ErrorCodes>>();
            container.RegisterType<IExceptionHandler<ErrorCodes>, ExceptionHandler<ErrorCodes>>();

            container.RegisterType<ICoreConfiguration, CoreConfiguration>();
            container.RegisterType<FeatureSettings>();
            container.RegisterType<FeatureSettingsSection>();
            var featureSettings = container.Resolve<FeatureSettings>();
            container.RegisterInstance(featureSettings);

            container.RegisterType<ICoreMessageHandler, CoreMessageHandler>();
            container.RegisterType<ICoreExceptionHandler, CoreExceptionHandler>();

            container.RegisterType<ICoreMessageHandler, CoreMessageHandler>();
            container.RegisterType<ICoreExceptionHandler, CoreExceptionHandler>();
        }
    }
}
