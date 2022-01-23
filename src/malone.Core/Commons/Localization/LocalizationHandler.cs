//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:05</date>

using System;
using System.Resources;

namespace malone.Core.Commons.Localization
{
    /// <summary>
    /// Defines the <see cref="LocalizationHandler{TCode}" />.
    /// </summary>
    /// <typeparam name="TCode">.</typeparam>
    public abstract class LocalizationHandler<TCode> : ILocalizationHandler<TCode>
        where TCode : Enum
    {
        /// <summary>
        /// Gets the ResourceManager.
        /// </summary>
        public abstract ResourceManager ResourceManager { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationHandler{TCode}"/> class.
        /// </summary>
        public LocalizationHandler()
        {
        }

        /// <summary>
        /// The GetString.
        /// </summary>
        /// <param name="code">The code<see cref="TCode"/>.</param>
        /// <param name="args">The args<see cref="object[]"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetString(TCode code, params object[] args)
        {
            string message = "";

            string errorKey = code.ToString();
            message = ResourceManager?.GetString(errorKey);

            if (message != null && args != null && args.Length > 0)
            {
                message = string.Format(message, args);
            }

            return message;
        }
    }
}
