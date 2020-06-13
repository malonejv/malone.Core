using malone.Core.AdoNet.Attributes;
using System;
using System.Data;

namespace malone.Core.AdoNet.Oracle.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class OracleResultSetAttribute : DbParameterAttribute
	{
		public OracleResultSetAttribute()
		{
			base.Direction = ParameterDirection.Output;
			//base.Type = 121;
		}
	}
}
