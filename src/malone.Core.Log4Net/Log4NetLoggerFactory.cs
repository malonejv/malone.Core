using log4net;
using log4net.Config;
using malone.Core.Logging;

namespace malone.Core.Log4Net
{
	public class Log4NetLoggerFactory : LoggerFactory
	{
		static Log4NetLoggerFactory()
		{
			// load the log4net configuration from the application configuration.
			XmlConfigurator.Configure();
		}

		public override ICoreLogger GetLogger()
		{
			return new Log4netLogger(LogManager.GetLogger(typeof(ILog)));
		}

		public override ICoreLogger GetLogger(string name)
		{
			return new Log4netLogger(LogManager.GetLogger(name));
		}
	}
}
