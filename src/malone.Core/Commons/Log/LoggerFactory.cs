//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:08</date>

namespace malone.Core.Commons.Log
{
                public abstract class LoggerFactory
    {
                                        public abstract ILogger GetLogger();

                                                public abstract ILogger GetLogger(string name);
    }
}
