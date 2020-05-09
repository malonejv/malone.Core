using malone.Core.DAL.AdoNet.Parameters;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.DAL.AdoNet.Provider.Oracle.Parameters.Oracle
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
