using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute("EFSqlServerApiStartup", typeof(malone.Core.Sample.EF.SqlServer.Api.Startup))]
namespace malone.Core.Sample.EF.SqlServer.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
