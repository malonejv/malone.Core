using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.DAL.AdoNet.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbParameterAttribute : DbFieldAttribute
    {
        private int _size;

        public DbParameterAttribute()
        {
            this.BindOnNull = true;
        }

        public int Order { get; set; }

        public ParameterDirection Direction { get; set; }

        public object Type { get; set; }

        public bool IsSizeDefined { get; private set; }

        public bool BindOnNull { get; set; }

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
