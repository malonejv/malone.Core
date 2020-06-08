using malone.Core.Commons.Exceptions.Manager;
using malone.Core.Commons.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Exceptions.Handler
{
    public class CoreExceptionHandler : ExceptionHandler<CoreErrors>, ICoreExceptionHandler
    {

        internal CoreExceptionHandler(ILogger logger, ICoreMessageHandler messageHandler)
            : base(logger, messageHandler) { }

    }
}
