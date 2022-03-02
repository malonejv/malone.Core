//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:09</date>

namespace malone.Core.Logging
{
	using System;
	using System.Xml.Serialization;

	/// <summary>
	/// Defines the <see cref="LogItem" />.
	/// </summary>
	public class LogItem
	{
		/// <summary>
		/// Gets or sets the LogLevel.
		/// </summary>
		public LogLevel LogLevel { get; set; }

		/// <summary>
		/// Gets or sets the Timestamp.
		/// </summary>
		public DateTimeOffset Timestamp { get; set; }

		/// <summary>
		/// Gets or sets the Title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the Message.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the LoggerName.
		/// </summary>
		public string LoggerName { get; set; }

		/// <summary>
		/// Gets or sets the Exception.
		/// </summary>
		[XmlIgnore]
		public Exception Exception { get; set; }

		/// <summary>
		/// Gets or sets the EventId.
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
		/// The Clone.
		/// </summary>
		/// <returns>The <see cref="LogItem"/>.</returns>
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
