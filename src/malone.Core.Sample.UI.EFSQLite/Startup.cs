using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(malone.Core.Sample.UI.EFSQLite.Startup))]
namespace malone.Core.Sample.UI.EFSQLite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
