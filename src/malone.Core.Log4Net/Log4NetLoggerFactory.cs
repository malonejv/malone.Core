﻿using log4net;
using log4net.Config;
using malone.Core.Commons.Log;

namespace malone.Core.Log4Net
{
    public class Log4NetLoggerFactory : LoggerFactory
    {
        static Log4NetLoggerFactory()
        {
            // load the log4net configuration from the application configuration.
            XmlConfigurator.Configure();
        }

        public override ILogger GetLogger()
        {
            return new Log4netLogger(LogManager.GetLogger(typeof(ILog)));
        }

        public override ILogger GetLogger(string name)
        {
            return new Log4netLogger(LogManager.GetLogger(name));
        }
    }
}
