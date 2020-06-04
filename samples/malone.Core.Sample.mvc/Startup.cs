using malone.Core.CL.DI.Unity;
using malone.Core.Sample.DI;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(malone.Core.Sample.mvc.Startup))]

namespace malone.Core.Sample.mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
