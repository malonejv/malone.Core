﻿using System.Collections.Generic;
using malone.Core.Commons.Initializers;
using malone.Core.Sample.AN.SqlServer.Middle.Initializers;
using Unity;

namespace malone.Core.Sample.AN.SqlServer.mvc.Initializers
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
                    new MvcLayerInitializer()
              };
            }
        }
    }
}
