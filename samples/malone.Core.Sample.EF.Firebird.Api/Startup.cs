using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute("EFFirebirdApiStartup", typeof(malone.Core.Sample.EF.Firebird.Api.Startup))]
namespace malone.Core.Sample.EF.Firebird.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
