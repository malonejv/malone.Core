using malone.Core.AdoNet.Parameters;
using malone.Core.Commons.Helpers.Extensions;

namespace malone.Core.AdoNet.Oracle.TypeConverters
{
    public class OracleVarcharToSecureString : IParameterConverter
    {
        public object Convert(object value)
        {
            return value != null ? value.ToString().ToSecureString() : null;
        }
    }
}
