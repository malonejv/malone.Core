using malone.Core.CL.Exceptions.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.EF.DAL.Exceptions
{
    internal class EFMessageHandler : MessageHandler<EFErrors>, IEFMessageHandler
    {
    }
}
