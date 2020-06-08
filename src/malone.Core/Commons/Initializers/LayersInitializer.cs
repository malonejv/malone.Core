using malone.Core.Commons.DI;
using System;
using System.Collections.Generic;

namespace malone.Core.Commons.Initializers
{

    public abstract class LayersInitializer<TContainer> : ILayerInitializer<TContainer>
    {
        public abstract IEnumerable<IInitializer<TContainer>> Layers { get; }

        /// <summary>
        ///     Sets the up.
        /// </summary>
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
