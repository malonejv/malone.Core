using malone.Core.Commons.Initializers;
using System.Collections.Generic;
using Unity;

namespace ErgaOmnes.Core.Initializers
{
    public class ErgaOmnesInitializer : LayersInitializer<IUnityContainer>
    {
        public override IEnumerable<IInitializer<IUnityContainer>> Layers
        {
            get
            {
                return new IInitializer<IUnityContainer>[]
              {
                    new CLInitializer(),
                    new ELInitializer(),
                    new DALInitializer(),
                    new BLInitializer(),
                    new SLInitializer()
              };
            }
        }
    }
}
