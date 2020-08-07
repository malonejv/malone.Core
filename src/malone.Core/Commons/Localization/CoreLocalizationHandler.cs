using System.Resources;

namespace malone.Core.Commons.Localization
{
    internal class ContentLocalizationHandler : LocalizationHandler<CoreContents>, IContentLocalizationHandler
    {
        public override ResourceManager ResourceManager => Resources.Contents.ResourceManager;
    }
}
