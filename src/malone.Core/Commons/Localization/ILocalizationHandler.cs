//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:05</date>

using System;

namespace malone.Core.Commons.Localization
{
    /// <summary>
    /// Defines the <see cref="ILocalizationHandler{TCode}" />.
    /// </summary>
    /// <typeparam name="TCode">.</typeparam>
    public interface ILocalizationHandler<TCode>
        where TCode : Enum
    {
        /// <summary>
        /// The GetString.
        /// </summary>
        /// <param name="code">The code<see cref="TCode"/>.</param>
        /// <param name="args">The args<see cref="object[]"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        string GetString(TCode code, params object[] args);
    }
}
