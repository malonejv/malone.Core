using malone.Core.Commons.Localization;
using System.Resources;

namespace malone.Core.Sample.AdoNet.Firebird.Middle.CL.Localization
{
    public class ContentLocalizationHandler : LocalizationHandler<ContentCode>, IContentLocalizationHandler
    {
        public override ResourceManager ResourceManager => Contents.ResourceManager;
    }
}
