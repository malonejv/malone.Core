﻿using malone.Core.Commons.Initializers;
using System.Collections.Generic;
using Unity;

namespace malone.Core.Sample.EF.SqlServer.Middle.Initializers
{
    public class SampleInitializer : LayersInitializer<IUnityContainer>
    {
        public SampleInitializer()
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
                    new BusinessLayerInitializer(),
                    new ServiceLayerInitializer()
              };
            }
        }
    }
}
