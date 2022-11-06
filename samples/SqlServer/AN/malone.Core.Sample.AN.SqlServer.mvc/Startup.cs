using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("EFSqlServerStartup", typeof(malone.Core.Sample.AN.SqlServer.mvc.Startup))]

namespace malone.Core.Sample.AN.SqlServer.mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
