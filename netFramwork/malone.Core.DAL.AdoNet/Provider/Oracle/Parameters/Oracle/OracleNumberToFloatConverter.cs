using malone.Core.DAL.AdoNet.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.DAL.AdoNet.Provider.Oracle.Parameters
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
