using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Initializers
{
    public interface IInjectorInitializer<TContainer>
    {
        TContainer Initialize();
        void Terminate();
    }
}
