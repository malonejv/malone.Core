using malone.Core.AdoNet.Parameters;
using Oracle.ManagedDataAccess.Types;

namespace malone.Core.AdoNet.Oracle.Parameters
{
	public class OracleDecimalToIntParameterConverter : IParameterConverter
	{
		public object Convert(object value)
		{
			return ((OracleDecimal)value).ToInt32();
		}
	}
}
