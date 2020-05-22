using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace malone.Core.CL.Configurations.Features
{

    public class FeatureElement : ConfigurationElement
    {

        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("allEnabled", IsRequired = true)]
        public bool AllEnabled
        {
            get { return (bool)this["allEnabled"]; }
            set { this["allEnabled"] = value; }
        }

        [ConfigurationProperty("behaviors")]
        public BehaviorElementCollection Behaviors
        {
            get { return (BehaviorElementCollection)this["behaviors"]; }
        }
    }
}
