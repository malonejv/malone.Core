using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace malone.Core.CL.Logger
{
    public class LogItem<TException>
        where TException : Exception
    {

        #region properties

        /// <summary>
        /// The logging level, which defaults to <see cref="Slf.LogLevel.Info"/>.
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Date and time of the log entry. If no explicitly
        /// set, this property provides the timestamp of
        /// the object's creation.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// A summarizing title for the logged entry. Defaults to
        /// <c>String.Empty</c>.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The logged message body. Defaults to
        /// <c>String.Empty</c>.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The name of the logger used to log this item.
        /// </summary>
        public string LoggerName { get; set; }

        /// <summary>
        /// Allows to attach an exception to the message.
        /// Defaults to <c>null</c>.
        /// </summary>
        [XmlIgnore]
        public TException Exception { get; set; }

        /// <summary>
        /// Event number or identifier. Defaults to null.
        /// </summary>
        public int? EventId { get; set; }

        #endregion


        /// <summary>
        /// Inits an new <see cref="LogItem"/> instance which
        /// is initialized with default values.
        /// </summary>
        public LogItem()
        {
            Title = string.Empty;
            Message = string.Empty;
            LogLevel = LogLevel.Info;
            LoggerName = string.Empty;

            Timestamp = DateTimeOffset.Now;
        }


        #region clone

        /// <summary>
        /// Creates a new <see cref="LogItem"/> that is a copy of the current instance.
        /// </summary>
        /// <returns>A new <c>LogItem</c> that is a copy of the current instance.</returns>
        public LogItem<TException> Clone()
        {
            LogItem<TException> clone = new LogItem<TException>
            {
                Title = Title,
                Message = Message,
                Exception = Exception,
                EventId = EventId,
                LogLevel = LogLevel,
                Timestamp = Timestamp,
                LoggerName = LoggerName
            };
            return clone;
        }

        #endregion

    }
}
