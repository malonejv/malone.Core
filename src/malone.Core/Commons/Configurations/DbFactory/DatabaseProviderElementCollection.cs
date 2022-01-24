//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:45</date>

using System.Configuration;

namespace malone.Core.Commons.Configurations.DbFactory
{
                [ConfigurationCollection(typeof(DatabaseProviderElement))]
    public class DatabaseProviderElementCollection : ConfigurationElementCollection
    {
                                        protected override ConfigurationElement CreateNewElement()
        {
            return new DatabaseProviderElement();
        }

                                                protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DatabaseProviderElement)element).Name;
        }
    }
}
