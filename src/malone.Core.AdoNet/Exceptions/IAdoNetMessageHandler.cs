using malone.Core.Commons.Exceptions.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.AdoNet.Exceptions
{
    internal interface IAdoNetMessageHandler : IMessageHandler<AdoNetErrors>
    {
    }
}
