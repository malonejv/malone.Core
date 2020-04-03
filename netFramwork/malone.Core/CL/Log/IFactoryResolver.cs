using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Log
{
    public enum LoggerFactories
    {
        Log4Net
    }

    public interface IFactoryResolver
    {
        LoggerFactory GetFactory(LoggerFactories factory);
    }
}
