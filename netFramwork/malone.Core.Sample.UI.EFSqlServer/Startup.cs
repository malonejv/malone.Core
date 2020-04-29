using Microsoft.Owin;
using Microsoft.Owin.Host.SystemWeb;
using Owin;
using System.Web;

//[assembly: OwinStartup(typeof(malone.Core.Sample.UI.EFSqlServer.Startup))]

namespace malone.Core.Sample.UI.EFSqlServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
