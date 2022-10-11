using System;

namespace malone.Core.Dapper.Attributes
{
	[System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class TableAttribute : Attribute
	{
		readonly string _name;

		public TableAttribute(string name)
		{
			_name = name;
		}

		public string Name
		{
			get { return _name; }
		}
	}
}
