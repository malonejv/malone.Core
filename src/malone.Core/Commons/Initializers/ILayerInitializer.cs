//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:02</date>

using malone.Core.Commons.Initializers;
using System.Collections.Generic;

namespace malone.Core.Commons.DI
{
                    public interface ILayerInitializer<TContainer> : IInitializer<TContainer>
    {
                                IEnumerable<IInitializer<TContainer>> Layers { get; }
    }
}
