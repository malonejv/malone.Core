//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:47</date>

using System.Configuration;

namespace malone.Core.Configuration.Features
{
	public class FeatureElement : ConfigurationElement
	{
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("allEnabled", IsRequired = true)]
		public bool AllEnabled
		{
			get { return (bool)this["allEnabled"]; }
			set { this["allEnabled"] = value; }
		}

		[ConfigurationProperty("behaviors")]
		public BehaviorElementCollection Behaviors
		{
			get { return (BehaviorElementCollection)this["behaviors"]; }
		}
	}
}
