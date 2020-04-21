using System;
using System.Threading.Tasks;
using malone.Core.Identity.BL.Components.MessageServices.Interfaces;
using malone.Core.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(malone.Core.Sample.UI.EFSqlServer.Startup))]

namespace malone.Core.Sample.UI.EFSqlServer
{
    public partial class Startup
    {
        public UserBusinessComponent UserBC { get; set; }
        public IEmailMessageService EmailService { get; set; }
        public ISmsMessageService SmsService { get; set; }

        public Startup(UserBusinessComponent userBC, IEmailMessageService emailService, ISmsMessageService smsService)
        {
            UserBC = UserBC;
            ConfigureUserManager();
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
