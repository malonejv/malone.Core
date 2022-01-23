//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:09</date>

using System;
using System.Xml.Serialization;

namespace malone.Core.Commons.Log
{
    /// <summary>
    /// Defines the <see cref="LogItem" />.
    /// </summary>
    public class LogItem
    {
        /// <summary>
        /// Gets or sets the LogLevel
        /// The logging level, which defaults to <see cref="Slf.LogLevel.Info"/>...
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the Timestamp
        /// Date and time of the log entry. If no explicitly
        /// set, this property provides the timestamp of
        /// the object's creation...
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the Title
        /// A summarizing title for the logged entry. Defaults to
        /// <c>String.Empty</c>...
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Message
        /// The logged message body. Defaults to
        /// <c>String.Empty</c>...
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the LoggerName
        /// The name of the logger used to log this item...
        /// </summary>
        public string LoggerName { get; set; }

        /// <summary>
        /// Gets or sets the Exception
        /// Allows to attach an exception to the message.
        /// Defaults to <c>null</c>...
        /// </summary>
        [XmlIgnore]
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the EventId
        /// Event number or identifier. Defaults to null...
        /// </summary>
        public int? EventId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogItem"/> class.
        /// </summary>
        public LogItem()
        {
            Title = string.Empty;
            Message = string.Empty;
            LogLevel = LogLevel.Info;
            LoggerName = string.Empty;

            Timestamp = DateTimeOffset.Now;
        }

        /// <summary>
        /// Creates a new <see cref="LogItem"/> that is a copy of the current instance.
        /// </summary>
        /// <returns>A new <c>LogItem</c> that is a copy of the current instance.</returns>
        public LogItem Clone()
        {
            LogItem clone = new LogItem
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
    }
}
