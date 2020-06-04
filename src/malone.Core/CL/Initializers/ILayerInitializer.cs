using malone.Core.CL.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.DI
{
    public interface ILayerInitializer<TContainer> : ILayer<TContainer>
    {
        IEnumerable<ILayer<TContainer>> Layers { get; }

    }
}
