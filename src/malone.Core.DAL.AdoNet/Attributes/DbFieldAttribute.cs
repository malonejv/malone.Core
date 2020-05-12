using malone.Core.DAL.AdoNet.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.DAL.AdoNet.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbFieldAttribute : Attribute
    {
        public string Name { get; set; }

        public Type ValueConverter { get; set; }

        public object DefaultValue { get; set; }

    }
}
