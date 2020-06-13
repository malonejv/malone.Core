using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Configurations.DbFactory
{
    public class DatabaseConfigurationElement : ConfigurationElement
    {

        [ConfigurationProperty("providers", IsRequired = true, IsKey = true)]
        public DatabaseProviderElementCollection Providers
        {
            get { return (DatabaseProviderElementCollection)this["providers"]; }
        }

    }
}
