//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:50</date>

using System;
using System.Configuration;

namespace malone.Core.Configuration.Modules
{
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
				{
				BaseRemoveAt(index);
				}

				BaseAdd(index, value);
			}
		}
		public void Add(ModuleElement element)
			{
			BaseAdd(element);
			}

		public void Clear()
			{
			BaseClear();
			}

		public void Remove(ModuleElement element)
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
			return new ModuleElement();
			}

		protected override object GetElementKey(ConfigurationElement element)
		{
		return ((ModuleElement)element).Name;
		}
	}
}
