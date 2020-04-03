using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.CL.Log;

namespace malone.Core.CL.Log.Log4Net
{
    public class Log4netLogger : LoggerBase
    {
        /// <summary>
        /// The log4net logger which this class wraps.
        /// </summary>
        private readonly ILog logger;

        /// <summary>
        /// Constructs an instance of <see cref="Log4netLogger"/>
        /// by wrapping a log4net logger
        /// </summary>
        /// <param name="logger">The log4net logger to wrap</param>
        public Log4netLogger(ILog logger) : base()
        {
            this.logger = logger;
        }

        /// <summary>
        /// Logs the given message. Output depends on the associated
        /// log4net configuration.
        /// </summary>
        /// <param name="item">A <see cref="LogItem"/> which encapsulates
        /// information to be logged.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="item"/>
        /// is a null reference.</exception>
        public override void Log<TException>(LogItem<TException> item) 
        {
            if (item == null) throw new ArgumentNullException("item");

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

        /// <summary>
        /// Overriden to delegate to the log4net IsXxxEnabled
        /// properties.
        /// </summary>
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
