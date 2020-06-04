using malone.Core.CL.DI;
using malone.Core.CL.Initializers;
using System.Collections.Generic;
using Unity;

namespace malone.Core.Sample.DI
{
    public class SampleInitializer : LayersInitializer<IUnityContainer>
    {
        public SampleInitializer()
        {
        }

        public override IEnumerable<ILayer<IUnityContainer>> Layers
        {
            get
            {
                return new ILayer<IUnityContainer>[]
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
