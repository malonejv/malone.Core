using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("EFFirebirdStartup", typeof(malone.Core.Sample.EF.Firebird.mvc.Startup))]

namespace malone.Core.Sample.EF.Firebird.mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
