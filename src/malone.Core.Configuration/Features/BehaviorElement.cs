//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:46</date>

using System.Configuration;

namespace malone.Core.Configuration.Features
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
