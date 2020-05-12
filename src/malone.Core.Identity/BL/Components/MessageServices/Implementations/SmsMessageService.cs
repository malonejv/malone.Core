using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Identity.BL.Components.MessageServices.Interfaces;

namespace malone.Core.Identity.BL.Components.MessageServices.Implementations
{

    public class SmsService : ISmsMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }
    }

}
