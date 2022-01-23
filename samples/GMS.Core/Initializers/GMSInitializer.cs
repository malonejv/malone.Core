using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Commons.Initializers;
using Unity;

namespace GMS.Core.Initializers
{
    public class GMSInitializer : LayersInitializer<IUnityContainer>
    {
        public GMSInitializer()
        {
        }

        public override IEnumerable<IInitializer<IUnityContainer>> Layers
        {
            get
            {
                return new IInitializer<IUnityContainer>[]
              {
                    new CommonLayerInitializer(),
                    new EntitiesLayerInitializer(),
                    new DataAccessLayerInitializer(),
                    new BusinessLayerInitializer()
              };
            }
        }
    }
}
