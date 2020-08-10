using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Commons.Localization;

namespace malone.Core.Sample.Middle.EL.Resource
{
    public class EntitiesLocalizationHandler : LocalizationHandler<EntitiesEnum>, IEntitiesLocalizationHandler
    {
        public override ResourceManager ResourceManager => Entities.ResourceManager;
    }
}
