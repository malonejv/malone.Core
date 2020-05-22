using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace malone.Core.CL.Configurations.Features
{
    public class BehaviorElement : ConfigurationElement
    {

        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("isEnabled", IsRequired = true)]
        public bool IsEnabled
        {
            get { return (bool)this["isEnabled"]; }
            set { this["isEnabled"] = value; }
        }
    }
}
