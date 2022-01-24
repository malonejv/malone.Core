//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:44</date>

using malone.Core.Commons.Configurations.Attributes;
using malone.Core.Commons.Configurations.DbFactory;
using malone.Core.Commons.Configurations.Modules;
using System;
using System.Configuration;

namespace malone.Core.Commons.Configurations
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
