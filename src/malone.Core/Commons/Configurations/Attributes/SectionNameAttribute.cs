//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:42</date>

using System;

namespace malone.Core.Commons.Configurations.Attributes
{
    /// <summary>
    /// Defines the <see cref="SectionNameAttribute" />.
    /// </summary>
    public class SectionNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SectionNameAttribute"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        public SectionNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the Name.
        /// </summary>
        public string Name { get; private set; }
    }
}
