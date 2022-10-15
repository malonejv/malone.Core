using malone.Core.Commons.Localization;
using malone.Core.Localization;
using System.Resources;

namespace malone.Core.Sample.EF.SqlServer.Middle.EL.Resource
{
    public class EntitiesLocalizationHandler : LocalizationHandler<EntitiesEnum>, IEntitiesLocalizationHandler
    {
        public override ResourceManager ResourceManager => Entities.ResourceManager;
    }
}
