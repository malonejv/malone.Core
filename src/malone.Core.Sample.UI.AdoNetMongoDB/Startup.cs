using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(malone.Core.Sample.UI.AdoNetMongoDB.Startup))]
namespace malone.Core.Sample.UI.AdoNetMongoDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
