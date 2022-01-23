using malone.Core.AdoNet.Parameters;
using Oracle.ManagedDataAccess.Types;

namespace malone.Core.AdoNet.Oracle.Parameters
{
    public class OracleStringToStringParameterConverter : IParameterConverter
    {
        public object Convert(object value)
        {
            OracleString oracleString = (OracleString)value;
            if (oracleString != null)
            {
                return oracleString.ToString();
            }
            return string.Empty;
        }
    }
}
