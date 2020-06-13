using log4net;
using malone.Core.Commons.Configurations;
using malone.Core.Commons.Configurations.Features;
using malone.Core.Commons.Exceptions.Handler;
using malone.Core.Commons.Exceptions.Manager;
using malone.Core.Commons.Initializers;
using malone.Core.Sample.Middle.CL.Exceptions;
using Unity;
using Unity.Injection;

namespace malone.Core.Sample.DI
{
    public class CommonLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {

            //container.RegisterType<ICoreConfiguration, CoreConfiguration>();
            //container.RegisterType<FeatureSettings>();
            //container.RegisterType<FeatureSettingsSection>();
            //var featureSettings = container.Resolve<FeatureSettings>();
            //container.RegisterInstance(featureSettings);

            //ILog logger = LogManager.GetLogger("SampleLogger");
            ////container.RegisterInstance<ILog>(logger);

            //container.RegisterType<LoggerFactory, Log4NetLoggerFactory>();
            //container.RegisterType<ILogger, Log4netLogger>(new InjectionConstructor(logger));

            container.RegisterType<IMessageHandler<ErrorCodes>, MessageHandler<ErrorCodes>>();
            container.RegisterType<IExceptionHandler<ErrorCodes>, ExceptionHandler<ErrorCodes>>();
            //container.RegisterType<IMessageHandler<CoreErrors>, MessageHandler<CoreErrors>>();
            //container.RegisterType<IExceptionHandler<CoreErrors>, ExceptionHandler<CoreErrors>>();



        }
    }
}
