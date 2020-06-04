using malone.Core.CL.Exceptions.Handler;
using malone.Core.CL.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.EF.DAL.Exceptions
{
    internal class EFExceptionHandler : ExceptionHandler<EFErrors>, IEFExceptionHandler
    {
        internal EFExceptionHandler(ILogger logger, IEFMessageHandler messageHandler)
            : base(logger, messageHandler) { }

    }
}
