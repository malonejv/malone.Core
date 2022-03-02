//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:06</date>

namespace malone.Core.Logging
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
