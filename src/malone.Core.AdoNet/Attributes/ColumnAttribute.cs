using System;
using System.Data;

namespace malone.Core.AdoNet.Attributes
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class ColumnAttribute : Attribute
    {
        private int _size;
		
        public ColumnAttribute(string name = null, DbType type = DbType.String, ParameterDirection direction = ParameterDirection.Input, bool isKey = false)
        {
	        Name = name;
	        Type = type;
	        Direction = direction;
	        IsKey = isKey;
        }

		public string Name { get; internal set; }

        public int Order { get; set; }

        public ParameterDirection Direction { get; set; }

        public DbType Type { get; set; }

        public bool IsKey { get; set; }

        public bool IsSizeDefined { get; private set; }

        public int Size
        {
            get
            {
                return this._size;
            }
            set
            {
                this._size = value;
                this.IsSizeDefined = true;
            }
        }

        public object Value { get; internal set; }

    }
}
