//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:09</date>

using System;
using System.Xml.Serialization;

namespace malone.Core.Logging
{
    public class LogItem
    {
        public LogLevel LogLevel { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string LoggerName { get; set; }

        [XmlIgnore]
        public Exception Exception { get; set; }

        public int? EventId { get; set; }

        public LogItem()
        {
            Title = string.Empty;
            Message = string.Empty;
            LogLevel = LogLevel.Info;
            LoggerName = string.Empty;

            Timestamp = DateTimeOffset.Now;
        }

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
