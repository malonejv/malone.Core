//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:47</date>

using System;

namespace malone.Core.Commons.Configurations.Features
{
    /// <summary>
    /// Defines the <see cref="FeatureDescriptionAttribute" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]

    public class FeatureDescriptionAttribute : System.Attribute
    {
        /// <summary>
        /// Gets the Feature.
        /// </summary>
        public string Feature { get; private set; }

        /// <summary>
        /// Gets the Behavior.
        /// </summary>
        public string Behavior { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="feature">The feature<see cref="string"/>.</param>
        /// <param name="behavior">The behavior<see cref="string"/>.</param>
        public FeatureDescriptionAttribute(string feature, string behavior = null)
        {

            Feature = feature;

            Behavior = behavior;
        }
    }
}
