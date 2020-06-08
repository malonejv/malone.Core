using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Configurations.Features
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]

    public class FeatureDescriptionAttribute : System.Attribute

    {

        /// <summary>

        /// Property of the code attribute

        /// </summary>

        public string Feature { get; private set; }

        public string Behavior { get; private set; }



        /// <summary>

        /// Describes a feature and behavior.

        /// </summary>

        /// <param name="code">Property code</param>

        public FeatureDescriptionAttribute(string feature, string behavior = null)

        {

            Feature = feature;

            Behavior = behavior;

        }

    }
}
