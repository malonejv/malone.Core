using malone.Core.AdoNet.DAL.Parameters;
using Oracle.ManagedDataAccess.Types;

namespace malone.Core.DAL.AdoNet.Oracle.Parameters
{
	public class OracleDecimalToIntParameterConverter : IParameterConverter
	{
		public object Convert(object value)
		{
			return ((OracleDecimal)value).ToInt32();
		}
	}
}
