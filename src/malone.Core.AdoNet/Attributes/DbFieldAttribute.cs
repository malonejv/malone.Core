using System;
using malone.Core.AdoNet.Parameters;

namespace malone.Core.AdoNet.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbFieldAttribute : Attribute
    {
        public DbFieldAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public Type ValueConverter { get; set; }

    }
}
