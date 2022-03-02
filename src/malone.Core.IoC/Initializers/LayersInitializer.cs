//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:03</date>

using System;
using System.Collections.Generic;
using malone.Core.Commons.DI;

namespace malone.Core.IoC.Initializers
{
    public abstract class LayersInitializer<TContainer> : ILayerInitializer<TContainer>
    {
        public abstract IEnumerable<IInitializer<TContainer>> Layers { get; }

        public void Initialize(TContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            foreach (var layer in Layers)
            {
                layer.Initialize(container);
            }
        }
    }
}
