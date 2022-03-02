//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:44</date>

using System;
using System.Configuration;
using malone.Core.Configuration.Attributes;
using malone.Core.Configuration.DbFactory;
using malone.Core.Configuration.Modules;

namespace malone.Core.Configuration
	{
	[SectionName("coreSettings")]
    public class CoreSettingsSection : ConfigurationSection
    {
        [ConfigurationProperty("xmlns", IsRequired = false)]
        public String Xmlns
        {
            get
            {
                return this["xmlns"] != null ? this["xmlns"].ToString() : string.Empty;
            }
        }

        [ConfigurationProperty("databaseConfiguration", IsDefaultCollection = true)]
        public DatabaseConfigurationElement DatabaseConfiguration
        {
            get { return (DatabaseConfigurationElement)this["databaseConfiguration"]; }
            set { this["databaseConfiguration"] = value; }
        }

        [ConfigurationProperty("modules", IsRequired = false)]
        public ModulesElementCollection Modules
        {
            get
            {
                return (ModulesElementCollection)this["modules"];
            }
        }
    }
}
