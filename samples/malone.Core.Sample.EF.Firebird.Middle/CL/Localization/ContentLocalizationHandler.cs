using System.Resources;
using malone.Core.Commons.Localization;

namespace malone.Core.Sample.EF.Firebird.Middle.CL.Localization
{
    public class ContentLocalizationHandler : LocalizationHandler<ContentCode>, IContentLocalizationHandler
    {
        public override ResourceManager ResourceManager => Contents.ResourceManager;
    }
}
