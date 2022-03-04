//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:02</date>

using System.Collections.Generic;
using malone.Core.IoC.Initializers;

namespace malone.Core.Commons.DI
	{
	public interface ILayerInitializer<TContainer> : IInitializer<TContainer>
    {
        IEnumerable<IInitializer<TContainer>> Layers { get; }
    }
}
