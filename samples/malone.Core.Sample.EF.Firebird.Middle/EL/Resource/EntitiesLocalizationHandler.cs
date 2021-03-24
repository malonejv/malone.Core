using malone.Core.Commons.Localization;
using System.Resources;

namespace malone.Core.Sample.EF.Firebird.Middle.EL.Resource
{
    public class EntitiesLocalizationHandler : LocalizationHandler<EntitiesEnum>, IEntitiesLocalizationHandler
    {
        public override ResourceManager ResourceManager => Entities.ResourceManager;
    }
}
