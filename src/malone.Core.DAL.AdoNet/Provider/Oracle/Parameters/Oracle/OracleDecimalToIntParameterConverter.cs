using malone.Core.DAL.AdoNet.Parameters;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.DAL.AdoNet.Provider.Oracle.Parameters
{
	public class OracleDecimalToIntParameterConverter : IParameterConverter
	{
		public object Convert(object value)
		{
			return ((OracleDecimal)value).ToInt32();
		}
	}
}
