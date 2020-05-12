using malone.Core.Identity.BL.Components.MessageServices.Interfaces;
using malone.Core.Identity.EntityFramework.EL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Identity.EntityFramework.BL
{
    public interface IUserManagerConfiguration
    {
        UserBusinessComponent UserBC { get; set; }
        IEmailMessageService EmailService { get; set; }
        ISmsMessageService SmsService { get; set; }

        void ConfigureUserManager();

    }
}
