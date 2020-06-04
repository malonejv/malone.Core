using malone.Core.CL.Exceptions.Manager;
using malone.Core.CL.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions.Handler
{
    public class CoreExceptionHandler : ExceptionHandler<CoreErrors>, ICoreExceptionHandler
    {

        internal CoreExceptionHandler(ILogger logger, ICoreMessageHandler messageHandler)
            : base(logger, messageHandler) { }

    }
}
