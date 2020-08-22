using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(malone.Core.Sample.AdoNet.SqlServer.Api.Startup))]
namespace malone.Core.Sample.AdoNet.SqlServer.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
