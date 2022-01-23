using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("AdoNetSqlServerStartup", typeof(malone.Core.Sample.AdoNet.SqlServer.mvc.Startup))]

namespace malone.Core.Sample.AdoNet.SqlServer.mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
