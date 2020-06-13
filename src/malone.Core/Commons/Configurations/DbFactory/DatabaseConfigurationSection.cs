using malone.Core.CL.Configurations.Attributes;
using System;

using System.Configuration;

namespace malone.Core.CL.Configurations.DbFactory
{
    //http://www.primaryobjects.com/2007/11/16/implementing-a-database-factory-pattern-in-c-asp-net/
    [SectionName("databaseConfiguration")]
    public sealed class DatabaseConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("providers", IsDefaultCollection = true)]
        public DatabaseProviderElementCollection Providers
        {
            get { return (DatabaseProviderElementCollection)this["providers"]; }
            set { this["providers"] = value; }
        }
    }
}
