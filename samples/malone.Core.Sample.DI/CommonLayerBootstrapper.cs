using log4net;
using malone.Core.CL.Configurations;
using malone.Core.CL.Configurations.Features;
using malone.Core.CL.DI;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.CL.Exceptions.Manager.Implementations;
using malone.Core.CL.Exceptions.Manager.Interfaces;
using malone.Core.CL.Log;
using malone.Core.CL.Log.Log4Net;
using malone.Core.CL.Logging.Log4Net;
using malone.Core.Sample.Middle.CL.Exceptions;
using Unity;
using Unity.Injection;
using System.Configuration;

namespace malone.Core.Sample.DI
{
    public class CommonLayerBootstrapper : ILayerBootstrapper<IUnityContainer>
    {
        public void RegisterTypes(IUnityContainer container)
        {

            container.RegisterType<ICoreConfiguration, CoreConfiguration>();
            container.RegisterType<FeatureSettings>();
            container.RegisterType<FeatureSettingsSection>();
            var featureSettings = container.Resolve<FeatureSettings>();
            container.RegisterInstance(featureSettings);

            ILog logger = LogManager.GetLogger("SampleLogger");
            //container.RegisterInstance<ILog>(logger);

            container.RegisterType<LoggerFactory, Log4NetLoggerFactory>();
            container.RegisterType<ILogger, Log4netLogger>(new InjectionConstructor(logger));

            container.RegisterType<IMessageHandler<ErrorCodes>, MessageHandler<ErrorCodes>>();
            container.RegisterType<IMessageHandler<CoreErrors>, MessageHandler<CoreErrors>>();
            container.RegisterType<IExceptionHandler<CoreErrors>, ExceptionHandler<CoreErrors>>();



        }
    }
}
