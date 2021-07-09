using System;
using System.Data;

namespace malone.Core.Dapper.Attributes
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class ParameterAttribute : Attribute
    {
        private int _size;

        public ParameterAttribute(string name = null, DbType type = DbType.String, ParameterDirection direction = ParameterDirection.Input, bool ignoreDbNull = false)
        {
            Name = name;
            Type = type;
            Direction = direction;
            IgnoreDbNull = ignoreDbNull;
        }

        public string Name { get; internal set; }

        public int Order { get; set; }

        public ParameterDirection Direction { get; set; }

        public DbType? Type { get; set; }

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

        public bool IgnoreDbNull { get; set; }

        public object Value { get; internal set; }
    }
}
