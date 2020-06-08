using malone.Core.Commons.Exceptions.Handler;
using malone.Core.Commons.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.EF.Exceptions
{
    public class EFExceptionHandler : ExceptionHandler<EFErrors>, IEFExceptionHandler
    {
        internal EFExceptionHandler(ILogger logger, IEFMessageHandler messageHandler)
            : base(logger, messageHandler) { }

    }
}
