using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(malone.Core.Sample.UI.AdoNetOracle.Startup))]
namespace malone.Core.Sample.UI.AdoNetOracle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
