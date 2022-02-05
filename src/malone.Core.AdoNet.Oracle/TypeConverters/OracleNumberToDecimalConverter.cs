using System;
using System.Globalization;
using malone.Core.AdoNet.Parameters;

namespace malone.Core.AdoNet.Oracle.Parameters
{
    public class OracleNumberToDecimalConverter : IParameterConverter
    {
        public object Convert(object value)
        {
            if (value is DBNull)
            {
                return 0m;
            }
            return System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);
        }
    }
}
