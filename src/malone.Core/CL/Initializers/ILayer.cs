using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Initializers
{
    public interface ILayer<TContainer>
    {
        void Initialize(TContainer container);
    }
}
