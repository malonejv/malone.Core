using malone.Core.Commons.Localization;
using malone.Core.Localization;
using System.Resources;

namespace malone.Core.Sample.AN.SqlServer.Middle.CL.Localization
{
    public class ContentLocalizationHandler : LocalizationHandler<ContentCode>, IContentLocalizationHandler
    {
        public override ResourceManager ResourceManager => Contents.ResourceManager;
    }
}
