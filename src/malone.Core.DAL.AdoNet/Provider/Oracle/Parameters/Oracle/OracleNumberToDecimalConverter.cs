using malone.Core.DAL.AdoNet.Parameters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.DAL.AdoNet.Provider.Oracle.Parameters
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
