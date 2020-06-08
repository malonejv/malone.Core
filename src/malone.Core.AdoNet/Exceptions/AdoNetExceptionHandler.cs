using malone.Core.Commons.Exceptions.Handler;
using malone.Core.Commons.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.AdoNet.Exceptions
{
    internal class AdoNetExceptionHandler : ExceptionHandler<AdoNetErrors>, IAdoNetExceptionHandler
    {
        internal AdoNetExceptionHandler(ILogger logger, IAdoNetMessageHandler messageHandler)
            : base(logger, messageHandler) { }

    }
}
