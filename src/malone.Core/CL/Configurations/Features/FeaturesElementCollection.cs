using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace malone.Core.CL.Configurations.Features
{
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

        public void Add(FeatureElement element)
        {
            BaseAdd(element);
        }

        public void Clear()
        {
            BaseClear();
        }

        public void Remove(FeatureElement element)
        {
            BaseRemove(element.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(String name)
        {
            BaseRemove(name);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FeatureElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FeatureElement)element).Name;
        }

    }
}
