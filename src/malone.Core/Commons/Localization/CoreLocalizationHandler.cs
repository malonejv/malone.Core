//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:04</date>

using System.Resources;
using malone.Core.Localization;

namespace malone.Core.Commons.Localization
{
    internal class ContentLocalizationHandler : LocalizationHandler<CoreContents>, IContentLocalizationHandler
    {
        public override ResourceManager ResourceManager => Resources.Contents.ResourceManager;
    }
}
