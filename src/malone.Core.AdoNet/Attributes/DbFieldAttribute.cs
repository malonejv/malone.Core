using System;

namespace malone.Core.AdoNet.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbFieldAttribute : Attribute
    {
        public string Name { get; set; }

        public Type ValueConverter { get; set; }

        public object DefaultValue { get; set; }

    }
}
