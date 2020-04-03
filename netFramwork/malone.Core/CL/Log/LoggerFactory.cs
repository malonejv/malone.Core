using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Log
{
    public abstract class LoggerFactory
    {
        public abstract ILogger GetLogger();
        public abstract ILogger GetLogger(string name);
    }
}
