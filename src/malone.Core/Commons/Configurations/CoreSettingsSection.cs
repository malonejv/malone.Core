using malone.Core.Commons.Configurations.Attributes;
using malone.Core.Commons.Configurations.DbFactory;
using malone.Core.Commons.Configurations.Features;
using malone.Core.Commons.Configurations.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
