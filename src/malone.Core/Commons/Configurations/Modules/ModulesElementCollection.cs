//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:50</date>

using System;
using System.Configuration;

namespace malone.Core.Commons.Configurations.Modules
{
    /// <summary>
    /// Defines the <see cref="ModulesElementCollection" />.
    /// </summary>
    [ConfigurationCollection(typeof(ModuleElement), AddItemName = "module", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class ModulesElementCollection : ConfigurationElementCollection
    {

        public ModuleElement this[int index]
        {
            get
            {
                return (ModuleElement)BaseGet(index);
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
        /// <param name="element">The element<see cref="ModuleElement"/>.</param>
        public void Add(ModuleElement element)
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
        /// <param name="element">The element<see cref="ModuleElement"/>.</param>
        public void Remove(ModuleElement element)
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
            return new ModuleElement();
        }

        /// <summary>
        /// The GetElementKey.
        /// </summary>
        /// <param name="element">The element<see cref="ConfigurationElement"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ModuleElement)element).Name;
        }
    }
}
