using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace malone.Core.CL.Configurations.Features
{

    [ConfigurationCollection(typeof(BehaviorElement), AddItemName = "behavior", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class BehaviorElementCollection : ConfigurationElementCollection
    {
        public BehaviorElement this[int index]
        {
            get { return (BehaviorElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        public void Add(BehaviorElement element)
        {
            BaseAdd(element);
        }

        public void Clear()
        {
            BaseClear();
        }

        public void Remove(BehaviorElement element)
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
            return new BehaviorElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BehaviorElement)element).Name;
        }
    }
}
