//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:08</date>

namespace malone.Core.Logging
{
    public abstract class LoggerFactory
    {
        public abstract ICoreLogger GetLogger();

        public abstract ICoreLogger GetLogger(string name);
    }
}
