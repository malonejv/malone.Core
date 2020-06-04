using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions.Manager
{
    public interface ICoreMessageHandler : IMessageHandler<CoreErrors>
    {
    }
}
