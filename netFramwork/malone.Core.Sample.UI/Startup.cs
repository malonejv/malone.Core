using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(malone.Core.Sample.UI.Startup))]
namespace malone.Core.Sample.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
