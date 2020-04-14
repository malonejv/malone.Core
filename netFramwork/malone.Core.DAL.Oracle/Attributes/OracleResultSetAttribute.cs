using malone.Core.AdoNet.DAL.Attributes;
using System;
using System.Data;

namespace malone.Core.DAL.AdoNet.Oracle.Attributes
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
