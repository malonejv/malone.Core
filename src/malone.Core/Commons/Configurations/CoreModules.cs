//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:43</date>

using System.ComponentModel;

namespace malone.Core.Commons.Configurations
	{
	public enum CoreModules
		{
		[Description("Basics")]
		Basics,
		[Description("Log4NetLogger")]
		Log4NetLogger,
		[Description("Features")]
		Features,
		[Description("IdentityAdoNetSqlServer")]
		IdentityAdoNetSqlServer,
		[Description("IdentityEntityFramework")]
		IdentityEntityFramework,
		[Description("IdentityDapper")]
		IdentityDapper,
		}
	}
