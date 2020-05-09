using malone.Core.AdoNet.DAL.Parameters;
using System;
using System.Globalization;

namespace malone.Core.DAL.AdoNet.Oracle.Parameters
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
