﻿using System;
using System.Data;

namespace malone.Core.AdoNet.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbParameterAttribute : DbFieldAttribute
    {
        private int _size;

        public DbParameterAttribute(string name) : base(name)
        {
        }

        public int Order { get; set; }

        public ParameterDirection Direction { get; set; }

        public object Type { get; set; }

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

    }
}
