//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:47</date>

using System;

namespace malone.Core.Commons.Configurations.Features
{
                [AttributeUsage(AttributeTargets.Method, Inherited = false)]

    public class FeatureDescriptionAttribute : System.Attribute
    {
                                public string Feature { get; private set; }

                                public string Behavior { get; private set; }

                                                public FeatureDescriptionAttribute(string feature, string behavior = null)
        {

            Feature = feature;

            Behavior = behavior;
        }
    }
}
