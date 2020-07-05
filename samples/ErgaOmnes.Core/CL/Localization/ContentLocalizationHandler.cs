using System.Resources;
using malone.Core.Commons.Localization;

namespace ErgaOmnes.Core.CL.Localization
{
    public class ContentLocalizationHandler : LocalizationHandler<ContentCode>, IContentLocalizationHandler
    {
        public override ResourceManager ResourceManager => Contents.ResourceManager;
    }
}
