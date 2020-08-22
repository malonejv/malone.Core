using malone.Core.AdoNet.Parameters;
using System;

namespace malone.Core.AdoNet.Oracle.Parameters
{
	public class OracleNumberToFloatConverter : IParameterConverter
	{
		public object Convert(object value)
		{
			if (value is DBNull)
			{
				return 0f;
			}
			return ((IConvertible)value).ToSingle(null);
		}
	}
}
