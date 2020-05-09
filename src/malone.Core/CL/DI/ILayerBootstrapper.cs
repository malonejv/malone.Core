using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.DI
{
    public interface ILayerBootstrapper<TContainer>
    {
        void RegisterTypes(TContainer container);
    }
}
