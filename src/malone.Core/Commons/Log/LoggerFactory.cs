//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:08</date>

namespace malone.Core.Commons.Log
{
    /// <summary>
    /// Defines the <see cref="LoggerFactory" />.
    /// </summary>
    public abstract class LoggerFactory
    {
        /// <summary>
        /// The GetLogger.
        /// </summary>
        /// <returns>The <see cref="ILogger"/>.</returns>
        public abstract ILogger GetLogger();

        /// <summary>
        /// The GetLogger.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="ILogger"/>.</returns>
        public abstract ILogger GetLogger(string name);
    }
}
