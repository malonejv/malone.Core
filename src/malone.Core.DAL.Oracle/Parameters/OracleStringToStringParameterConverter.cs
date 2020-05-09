using malone.Core.AdoNet.DAL.Parameters;
using Oracle.ManagedDataAccess.Types;

namespace malone.Core.DAL.AdoNet.Oracle.Parameters
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
