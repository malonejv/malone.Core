using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(malone.Core.Sample.EF.SqlServer.Api.Startup))]
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
