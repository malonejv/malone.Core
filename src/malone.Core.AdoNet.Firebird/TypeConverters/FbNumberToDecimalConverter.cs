using malone.Core.AdoNet.Parameters;
using System;
using System.Globalization;

namespace malone.Core.AdoNet.Oracle.Parameters
{
	public class FbNumberToDecimalConverter : IParameterConverter
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
