using log4net;
using malone.Core.CL.Configurations;
using malone.Core.CL.Configurations.CoreConfiguration;
using malone.Core.CL.Configurations.Sections.Feature;
using malone.Core.CL.DI;
using malone.Core.CL.Exceptions.Handler.Implementations;
using malone.Core.CL.Exceptions.Handler.Interfaces;
using malone.Core.CL.Exceptions.Manager.Implementations;
using malone.Core.CL.Exceptions.Manager.Interfaces;
using malone.Core.CL.Log;
using malone.Core.CL.Log.Log4Net;
using malone.Core.CL.Logging.Log4Net;
using Unity;
using Unity.Injection;

namespace malone.Core.Sample.DI 
{
    public class CommonLayerBootstrapper : ILayerBootstrapper<IUnityContainer>
    {
        public void RegisterTypes(IUnityContainer container)
        {

            container.RegisterType<ICoreSettingConfiguration, CoreSettingConfiguration>();
            container.RegisterType<FeatureSettings>();
            var featureSettings = container.Resolve<FeatureSettings>();
            container.RegisterInstance(featureSettings);

            ILog logger = LogManager.GetLogger("SampleLogger");
            //container.RegisterInstance<ILog>(logger);

            container.RegisterType<LoggerFactory, Log4NetLoggerFactory>();
            container.RegisterType<ILogger, Log4netLogger>(new InjectionConstructor(logger));

            container.RegisterType<IExceptionMessageManager, ExceptionMessageManager>();
            container.RegisterType<IExceptionHandler, ExceptionHandler>();



        }
    }
}
