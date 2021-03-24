//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:04</date>

using System.Resources;

namespace malone.Core.Commons.Localization
{
    /// <summary>
    /// Defines the <see cref="ContentLocalizationHandler" />.
    /// </summary>
    internal class ContentLocalizationHandler : LocalizationHandler<CoreContents>, IContentLocalizationHandler
    {
        /// <summary>
        /// Gets the ResourceManager.
        /// </summary>
        public override ResourceManager ResourceManager => Resources.Contents.ResourceManager;
    }
}
