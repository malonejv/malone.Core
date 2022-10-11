//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:50</date>

using System.Configuration;

namespace malone.Core.Configuration.Modules
{
	public class ModuleElement : ConfigurationElement
	{
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}
	}
}
