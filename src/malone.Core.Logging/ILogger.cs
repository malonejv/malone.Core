//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:07</date>

namespace malone.Core.Logging
{
	using System;

	/// <summary>
	/// Defines the <see cref="ICoreLogger" />.
	/// </summary>
	public interface ICoreLogger
	{
		/// <summary>
		/// Gets the Name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="obj">The obj<see cref="object"/>.</param>
		void Debug(object obj);

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="message">The message<see cref="string"/>.</param>
		void Debug(string message);

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Debug(string format, params object[] args);

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Debug(IFormatProvider provider, string format, params object[] args);

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		void Debug(Exception exception);

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="message">The message<see cref="string"/>.</param>
		void Debug(Exception exception, string message);

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Debug(Exception exception, string format, params object[] args);

		/// <summary>
		/// The Debug.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Debug(Exception exception, string format, IFormatProvider provider, params object[] args);

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="obj">The obj<see cref="object"/>.</param>
		void Info(object obj);

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="message">The message<see cref="string"/>.</param>
		void Info(string message);

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Info(string format, params object[] args);

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Info(IFormatProvider provider, string format, params object[] args);

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		void Info(Exception exception);

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="message">The message<see cref="string"/>.</param>
		void Info(Exception exception, string message);

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Info(Exception exception, string format, params object[] args);

		/// <summary>
		/// The Info.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Info(Exception exception, string format, IFormatProvider provider, params object[] args);

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="obj">The obj<see cref="object"/>.</param>
		void Warn(object obj);

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="message">The message<see cref="string"/>.</param>
		void Warn(string message);

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Warn(string format, params object[] args);

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Warn(IFormatProvider provider, string format, params object[] args);

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		void Warn(Exception exception);

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="message">The message<see cref="string"/>.</param>
		void Warn(Exception exception, string message);

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Warn(Exception exception, string format, params object[] args);

		/// <summary>
		/// The Warn.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Warn(Exception exception, string format, IFormatProvider provider, params object[] args);

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="obj">The obj<see cref="object"/>.</param>
		void Error(object obj);

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="message">The message<see cref="string"/>.</param>
		void Error(string message);

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Error(string format, params object[] args);

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Error(IFormatProvider provider, string format, params object[] args);

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		void Error(Exception exception);

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="message">The message<see cref="string"/>.</param>
		void Error(Exception exception, string message);

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Error(Exception exception, string format, params object[] args);

		/// <summary>
		/// The Error.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Error(Exception exception, string format, IFormatProvider provider, params object[] args);

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="obj">The obj<see cref="object"/>.</param>
		void Fatal(object obj);

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="message">The message<see cref="string"/>.</param>
		void Fatal(string message);

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Fatal(string format, params object[] args);

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Fatal(IFormatProvider provider, string format, params object[] args);

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		void Fatal(Exception exception);

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="message">The message<see cref="string"/>.</param>
		void Fatal(Exception exception, string message);

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Fatal(Exception exception, string format, params object[] args);

		/// <summary>
		/// The Fatal.
		/// </summary>
		/// <param name="exception">The exception<see cref="Exception"/>.</param>
		/// <param name="format">The format<see cref="string"/>.</param>
		/// <param name="provider">The provider<see cref="IFormatProvider"/>.</param>
		/// <param name="args">The args<see cref="T: object[]"/>.</param>
		void Fatal(Exception exception, string format, IFormatProvider provider, params object[] args);

		/// <summary>
		/// The Log.
		/// </summary>
		/// <param name="item">The item<see cref="LogItem"/>.</param>
		void Log(LogItem item);
	}
}
