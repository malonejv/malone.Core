//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:42</date>

using System;

namespace malone.Core.Configuration.Attributes
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
