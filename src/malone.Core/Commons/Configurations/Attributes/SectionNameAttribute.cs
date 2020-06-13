using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Configurations.Attributes
{
    public class SectionNameAttribute : Attribute
    {
        public SectionNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
