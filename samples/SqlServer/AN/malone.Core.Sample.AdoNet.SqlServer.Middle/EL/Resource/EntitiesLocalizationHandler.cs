using malone.Core.Commons.Localization;
using System.Resources;
using malone.Core.Localization;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Resource
{
    public class EntitiesLocalizationHandler : LocalizationHandler<EntitiesEnum>, IEntitiesLocalizationHandler
    {
        public override ResourceManager ResourceManager => Entities.ResourceManager;
    }
}
