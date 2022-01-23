//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:09</date>

namespace malone.Core.Commons.Log
{
    /// <summary>
    /// Defines the LogLevel.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// The logging level is undefined. This is regarded
        /// an invalid value.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Logs debugging output.
        /// </summary>
        Debug = 1,
        /// <summary>
        /// Logs basic information.
        /// </summary>
        Info = 2,
        /// <summary>
        /// Logs a warning.
        /// </summary>
        Warn = 3,
        /// <summary>
        /// Logs an error.
        /// </summary>
        Error = 4,
        /// <summary>
        /// Logs a fatal incident.
        /// </summary>
        Fatal = 5
    }
}
