using FirebirdSql.Data.FirebirdClient;
using malone.Core.AdoNet.Parameters;

namespace malone.Core.AdoNet.Oracle.Parameters
{
    public class FbDecimalToIntParameterConverter : IParameterConverter
	{
		public object Convert(object value)
		{
			var fbType = ((FbDbType)value);
			
			return System.Convert.ToDecimal(fbType.ToString());
		}
	}
}
