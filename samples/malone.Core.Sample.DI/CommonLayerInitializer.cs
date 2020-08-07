using malone.Core.Commons.Initializers;
using malone.Core.Commons.Localization;
using malone.Core.Sample.Middle.CL.Exceptions;
using Unity;

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

            container.RegisterType<ILocalizationHandler<ErrorCodes>, LocalizationHandler<ErrorCodes>>();
            //container.RegisterType<IMessageHandler<CoreErrors>, MessageHandler<CoreErrors>>();
            //container.RegisterType<IExceptionHandler<CoreErrors>, ExceptionHandler<CoreErrors>>();



        }
    }
}
