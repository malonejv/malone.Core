using malone.Core.Commons.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.DI
{
    public interface ILayerInitializer<TContainer> : IInitializer<TContainer>
    {
        IEnumerable<IInitializer<TContainer>> Layers { get; }

    }
}
