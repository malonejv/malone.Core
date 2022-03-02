//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:08</date>

namespace malone.Core.Logging
{
	/// <summary>
	/// Defines the <see cref="LoggerFactory" />.
	/// </summary>
	public abstract class LoggerFactory
	{
		/// <summary>
		/// The GetLogger.
		/// </summary>
		/// <returns>The <see cref="ICoreLogger"/>.</returns>
		public abstract ICoreLogger GetLogger();

		/// <summary>
		/// The GetLogger.
		/// </summary>
		/// <param name="name">The name<see cref="string"/>.</param>
		/// <returns>The <see cref="ICoreLogger"/>.</returns>
		public abstract ICoreLogger GetLogger(string name);
	}
}
