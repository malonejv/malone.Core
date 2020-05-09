using malone.Core.CL.DI;
using malone.Core.CL.DI.Unity;
using malone.Core.Patterns.Behavioral.Singleton;
using System.Collections.Generic;
using Unity;

namespace malone.Core.Sample.DI
{
    public class SampleBootstrapper : CoreBootstrapper<IUnityContainer, SampleBootstrapper>, ICoreRegistrator<IUnityContainer,SampleBootstrapper>
    {
        public SampleBootstrapper()
        {
        }

        protected override IEnumerable<ILayerBootstrapper<IUnityContainer>> CoreLayerBootstrappers
        {
            get
            {
                return new ILayerBootstrapper<IUnityContainer>[]
              {
                    new CommonLayerBootstrapper(),
                    new EntitiesLayerBootstrapper(),
                    new BusinessLayerBootstrapper(),
                    new DataAccessLayerBootstrapper(),
              };
            }
        }

    }
}
