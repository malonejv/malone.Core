using System;
using log4net;
using malone.Core.Commons.Log;

namespace malone.Core.Log4Net
{
    public class Log4netLogger : LoggerBase
    {
        private readonly ILog logger;

        public Log4netLogger(ILog logger) : base()
        {
            this.logger = logger;
        }

        public override void Log(LogItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            string message = item.Message;// FormatItem(item);

            switch (item.LogLevel)
            {
                case LogLevel.Fatal:
                    logger.Fatal(message, item.Exception);
                    break;

                case LogLevel.Error:
                    logger.Error(message, item.Exception);
                    break;

                case LogLevel.Warn:
                    logger.Warn(message, item.Exception);
                    break;

                case LogLevel.Info:
                    logger.Info(message, item.Exception);
                    break;

                case LogLevel.Debug:
                    logger.Debug(message, item.Exception);
                    break;

                default:
                    logger.Info(message, item.Exception);
                    break;
            }
        }

        protected override bool IsLogLevelEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return logger.IsDebugEnabled;
                case LogLevel.Error:
                    return logger.IsErrorEnabled;
                case LogLevel.Fatal:
                    return logger.IsFatalEnabled;
                case LogLevel.Info:
                    return logger.IsInfoEnabled;
                case LogLevel.Warn:
                    return logger.IsWarnEnabled;
                default:
                    return true;
            }
        }

    }
}
