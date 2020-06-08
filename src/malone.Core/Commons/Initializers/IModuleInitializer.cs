using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Initializers
{
    public interface IModuleInitializer<TContainer> : IInitializer<TContainer>
    {
        string Name { get; }

    }
}
