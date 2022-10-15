using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("AdoNetFirebirdStartup", typeof(malone.Core.Sample.AdoNet.Firebird.mvc.Startup))]

namespace malone.Core.Sample.AdoNet.Firebird.mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
