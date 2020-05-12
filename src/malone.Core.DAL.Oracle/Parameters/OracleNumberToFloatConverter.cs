using malone.Core.AdoNet.DAL.Parameters;
using System;

namespace malone.Core.DAL.AdoNet.Oracle.Parameters
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
