using malone.Core.CL.Initializers;
using System;
using System.Collections.Generic;
using Unity;

namespace ErgaOmnes.Core.Initializers
{
    public class ErgaOmnesInitializer : LayersInitializer<IUnityContainer>
    {
        public override IEnumerable<ILayer<IUnityContainer>> Layers
        {
            get
            {
                return new ILayer<IUnityContainer>[]
              {
                    new CLInitializer(),
                    new ELInitializer(),
                    new DALInitializer(),
                    new BLInitializer()
              };
            }
        }
    }
}
