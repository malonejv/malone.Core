//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:45</date>

using System.Configuration;

namespace malone.Core.Commons.Configurations.DbFactory
{
    /// <summary>
    /// Defines the <see cref="DatabaseProviderElementCollection" />.
    /// </summary>
    [ConfigurationCollection(typeof(DatabaseProviderElement))]
    public class DatabaseProviderElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// The CreateNewElement.
        /// </summary>
        /// <returns>The <see cref="ConfigurationElement"/>.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new DatabaseProviderElement();
        }

        /// <summary>
        /// The GetElementKey.
        /// </summary>
        /// <param name="element">The element<see cref="ConfigurationElement"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DatabaseProviderElement)element).Name;
        }
    }
}
