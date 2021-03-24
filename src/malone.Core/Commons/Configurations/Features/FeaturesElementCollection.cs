//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:48</date>

using System;
using System.Configuration;

namespace malone.Core.Commons.Configurations.Features
{
    /// <summary>
    /// Defines the <see cref="FeaturesElementCollection" />.
    /// </summary>
    [ConfigurationCollection(typeof(FeatureElement), AddItemName = "feature", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class FeaturesElementCollection : ConfigurationElementCollection
    {

        public FeatureElement this[int index]
        {
            get
            {
                return (FeatureElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }
        /// <summary>
        /// The Add.
        /// </summary>
        /// <param name="element">The element<see cref="FeatureElement"/>.</param>
        public void Add(FeatureElement element)
        {
            BaseAdd(element);
        }

        /// <summary>
        /// The Clear.
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }

        /// <summary>
        /// The Remove.
        /// </summary>
        /// <param name="element">The element<see cref="FeatureElement"/>.</param>
        public void Remove(FeatureElement element)
        {
            BaseRemove(element.Name);
        }

        /// <summary>
        /// The RemoveAt.
        /// </summary>
        /// <param name="index">The index<see cref="int"/>.</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// The Remove.
        /// </summary>
        /// <param name="name">The name<see cref="String"/>.</param>
        public void Remove(String name)
        {
            BaseRemove(name);
        }

        /// <summary>
        /// The CreateNewElement.
        /// </summary>
        /// <returns>The <see cref="ConfigurationElement"/>.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new FeatureElement();
        }

        /// <summary>
        /// The GetElementKey.
        /// </summary>
        /// <param name="element">The element<see cref="ConfigurationElement"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FeatureElement)element).Name;
        }
    }
}
