//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:08</date>

namespace malone.Core.Logging
{
	using System;

	/// <summary>
	/// Defines the <see cref="LoggerBase" />.
	/// </summary>
	public abstract class LoggerBase : ICoreLogger
	{
		/// <summary>
		/// Defines the NullString.
		/// </summary>
		private const string NullString = "null";

		/// <summary>
		/// Gets the Name.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggerBase"/> class.
		/// </summary>
		/// <param name="name">The name<see cref="string"/>.</param>
		protected LoggerBase(string name)
		{
			Name = name;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggerBase"/> class.
		/// </summary>
		protected LoggerBase()
		{
		}

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="obj">The obj<see cref="object"/>.</param>
		public void Debug(object obj)
		{
			if (IsLogLevelEnabled(LogLevel.Debug))
			{
				Log(LogLevel.Debug, obj != null ? obj.ToString() : NullString);
			}
		}

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="message">The message<see cref="string"/>.</param>
		public void Debug(string message)
		{
			if (IsLogLevelEnabled(LogLevel.Debug))
			{
				Log(LogLevel.Debug, message);
			}
		}

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Debug(string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Debug))
			{
				Log(LogLevel.Debug, format, args);
			}
		}

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Debug(IFormatProvider provider, string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Debug))
			{
				Log(LogLevel.Debug, provider, format, args);
			}
		}

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		public void Debug(Exception exception)
		{
			if (IsLogLevelEnabled(LogLevel.Debug))
			{
				Log(LogLevel.Debug, exception);
			}
		}

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="message">The message<see cref="string"/>.</param>
		public void Debug(Exception exception, string message)
		{
			if (IsLogLevelEnabled(LogLevel.Debug))
			{
				Log(LogLevel.Debug, exception, message);
			}
		}

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Debug(Exception exception, string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Debug))
			{
				Log(LogLevel.Debug, exception, format, args);
			}
		}

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Debug(Exception exception, string format, IFormatProvider provider, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Debug))
			{
				Log(LogLevel.Debug, exception, provider, format, args);
			}
		}

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="obj">The obj<see cref="object"/>.</param>
		public void Info(object obj)
		{
			if (IsLogLevelEnabled(LogLevel.Info))
			{
				Log(LogLevel.Info, obj != null ? obj.ToString() : NullString);
			}
		}

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="message">The message<see cref="string"/>.</param>
		public void Info(string message)
		{
			if (IsLogLevelEnabled(LogLevel.Info))
			{
				Log(LogLevel.Info, message);
			}
		}

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Info(string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Info))
			{
				Log(LogLevel.Info, format, args);
			}
		}

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Info(IFormatProvider provider, string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Info))
			{
				Log(LogLevel.Info, provider, format, args);
			}
		}

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		public void Info(Exception exception)
		{
			if (IsLogLevelEnabled(LogLevel.Info))
			{
				Log(LogLevel.Info, exception);
			}
		}

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="message">The message<see cref="string"/>.</param>
		public void Info(Exception exception, string message)
		{
			if (IsLogLevelEnabled(LogLevel.Info))
			{
				Log(LogLevel.Info, exception, message);
			}
		}

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Info(Exception exception, string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Info))
			{
				Log(LogLevel.Info, exception, format, args);
			}
		}

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Info(Exception exception, string format, IFormatProvider provider, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Info))
			{
				Log(LogLevel.Info, exception, provider, format, args);
			}
		}

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="obj">The obj<see cref="object"/>.</param>
		public void Warn(object obj)
		{
			if (IsLogLevelEnabled(LogLevel.Warn))
			{
				Log(LogLevel.Warn, obj != null ? obj.ToString() : NullString);
			}
		}

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="message">The message<see cref="string"/>.</param>
		public void Warn(string message)
		{
			if (IsLogLevelEnabled(LogLevel.Warn))
			{
				Log(LogLevel.Warn, message);
			}
		}

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Warn(string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Warn))
			{
				Log(LogLevel.Warn, format, args);
			}
		}

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Warn(IFormatProvider provider, string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Warn))
			{
				Log(LogLevel.Warn, provider, format, args);
			}
		}

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		public void Warn(Exception exception)
		{
			if (IsLogLevelEnabled(LogLevel.Warn))
			{
				Log(LogLevel.Warn, exception);
			}
		}

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="message">The message<see cref="string"/>.</param>
		public void Warn(Exception exception, string message)
		{
			if (IsLogLevelEnabled(LogLevel.Warn))
			{
				Log(LogLevel.Warn, exception, message);
			}
		}

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Warn(Exception exception, string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Warn))
			{
				Log(LogLevel.Warn, exception, format, args);
			}
		}

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Warn(Exception exception, string format, IFormatProvider provider, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Warn))
			{
				Log(LogLevel.Warn, exception, provider, format, args);
			}
		}

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="obj">The obj<see cref="object"/>.</param>
		public void Error(object obj)
		{
			if (IsLogLevelEnabled(LogLevel.Error))
			{
				Log(LogLevel.Error, obj != null ? obj.ToString() : NullString);
			}
		}

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="message">The message<see cref="string"/>.</param>
		public void Error(string message)
		{
			if (IsLogLevelEnabled(LogLevel.Error))
			{
				Log(LogLevel.Error, message);
			}
		}

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Error(string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Error))
			{
				Log(LogLevel.Error, format, args);
			}
		}

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Error(IFormatProvider provider, string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Error))
			{
				Log(LogLevel.Error, provider, format, args);
			}
		}

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		public void Error(Exception exception)
		{
			if (IsLogLevelEnabled(LogLevel.Error))
			{
				Log(LogLevel.Error, exception);
			}
		}

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="message">The message<see cref="string"/>.</param>
		public void Error(Exception exception, string message)
		{
			if (IsLogLevelEnabled(LogLevel.Error))
			{
				Log(LogLevel.Error, exception, message);
			}
		}

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Error(Exception exception, string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Error))
			{
				Log(LogLevel.Error, exception, format, args);
			}
		}

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Error(Exception exception, string format, IFormatProvider provider, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Error))
			{
				Log(LogLevel.Error, exception, provider, format, args);
			}
		}

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="obj">The obj<see cref="object"/>.</param>
		public void Fatal(object obj)
		{
			if (IsLogLevelEnabled(LogLevel.Fatal))
			{
				Log(LogLevel.Fatal, obj != null ? obj.ToString() : NullString);
			}
		}

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="message">The message<see cref="string"/>.</param>
		public void Fatal(string message)
		{
			if (IsLogLevelEnabled(LogLevel.Fatal))
			{
				Log(LogLevel.Fatal, message);
			}
		}

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Fatal(string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Fatal))
			{
				Log(LogLevel.Fatal, format, args);
			}
		}

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Fatal(IFormatProvider provider, string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Fatal))
			{
				Log(LogLevel.Fatal, provider, format, args);
			}
		}

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		public void Fatal(Exception exception)
		{
			if (IsLogLevelEnabled(LogLevel.Fatal))
			{
				Log(LogLevel.Fatal, exception);
			}
		}

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="message">The message<see cref="string"/>.</param>
		public void Fatal(Exception exception, string message)
		{
			if (IsLogLevelEnabled(LogLevel.Fatal))
			{
				Log(LogLevel.Fatal, exception, message);
			}
		}

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Fatal(Exception exception, string format, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Fatal))
			{
				Log(LogLevel.Fatal, exception, format, args);
			}
		}

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public void Fatal(Exception exception, string format, IFormatProvider provider, params object[] args)
		{
			if (IsLogLevelEnabled(LogLevel.Fatal))
			{
				Log(LogLevel.Fatal, exception, provider, format, args);
			}
		}

		/// <summary>
		/// The Log.
		/// </summary>
		/// <param name="level">The level<see cref="LogLevel"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public virtual void Log(LogLevel level, string format, params object[] args)
		{
			Log(level, String.Format(format, args));
		}

		/// <summary>
		/// The Log.
		/// </summary>
		/// <param name="level">The level<see cref="LogLevel"/>.</param>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public virtual void Log(LogLevel level, IFormatProvider provider, string format, params object[] args)
		{
			Log(level, String.Format(provider, format, args));
		}

		/// <summary>
		/// The Log.
		/// </summary>
		/// <param name="level">The level<see cref="LogLevel"/>.</param>
		/// <param name="message">The message<see cref="string"/>.</param>
		public virtual void Log(LogLevel level, string message)
		{
			LogItem item = new LogItem { LogLevel = level, Message = message, LoggerName = Name };
			Log(item);
		}

		/// <summary>
		/// The Log.
		/// </summary>
		/// <param name="level">The level<see cref="LogLevel"/>.</param>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		public virtual void Log(LogLevel level, Exception exception)
		{
			Log(level, exception, String.Empty);
		}

		/// <summary>
		/// The Log.
		/// </summary>
		/// <param name="level">The level<see cref="LogLevel"/>.</param>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public virtual void Log(LogLevel level, Exception exception, string format, params object[] args)
		{
			string message = String.Format(format, args);
			Log(level, exception, message);
		}

		/// <summary>
		/// The Log.
		/// </summary>
		/// <param name="level">The level<see cref="LogLevel"/>.</param>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		public virtual void Log(LogLevel level, Exception exception, IFormatProvider provider, string format,
params object[] args)
		{
			string message = String.Format(provider, format, args);
			Log(level, exception, message);
		}

		/// <summary>
		/// The Log.
		/// </summary>
		/// <param name="level">The level<see cref="LogLevel"/>.</param>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="message">The message<see cref="string"/>.</param>
		public virtual void Log(LogLevel level, Exception exception, string message)
		{
			Type exceptionType = typeof(Exception);
			var errorMessagePropertyInfo = exceptionType.GetProperty("ErrorMessage");
			if (errorMessagePropertyInfo != null)
			{
				message = errorMessagePropertyInfo.GetValue(exception).ToString();
			}


			LogItem item = new LogItem { LogLevel = level, Message = message, LoggerName = this.Name };
			item.Exception = exception;
			Log(item);
		}

		/// <summary>
		/// The Log.
		/// </summary>
		/// <param name="item">The item<see cref="LogItem"/>.</param>
		public abstract void Log(LogItem item);

		/// <summary>
		/// The IsLogLevelEnabled.
		/// </summary>
		/// <param name="level">The level<see cref="LogLevel"/>.</param>
		/// <returns>The <see cref="bool"/>.</returns>
		protected virtual bool IsLogLevelEnabled(LogLevel level)
		{
			return true;
		}
	}
}
