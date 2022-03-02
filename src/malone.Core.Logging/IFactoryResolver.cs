﻿//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:06</date>

namespace malone.Core.Logging
{
	/// <summary>
	/// Defines the LoggerFactories.
	/// </summary>
	public enum LoggerFactories
	{
		/// <summary>
		/// Defines the Log4Net.
		/// </summary>
		Log4Net
	}

	/// <summary>
	/// Defines the <see cref="T: IFactoryResolver" />.
	/// </summary>
	public interface IFactoryResolver
	{
		/// <summary>
		/// The GetFactory.
		/// </summary>
		/// <param name="factory">The factory<see cref="T: LoggerFactories"/>.</param>
		/// <returns>The <see cref="T: LoggerFactory"/>.</returns>
		LoggerFactory GetFactory(LoggerFactories factory);
	}
}
