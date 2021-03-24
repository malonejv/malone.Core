//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:54</date>

using malone.Core.Commons.Localization;
using System.Resources;

namespace malone.Core.Commons.Exceptions
{
    /// <summary>
    /// Defines the <see cref="ErrorLocalizationHandler" />.
    /// </summary>
    internal class ErrorLocalizationHandler : LocalizationHandler<CoreErrors>, IErrorLocalizationHandler
    {
        /// <summary>
        /// Gets the ResourceManager.
        /// </summary>
        public override ResourceManager ResourceManager => Resources.Exceptions.ResourceManager;
    }
}
