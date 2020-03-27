using malone.Core.BL.Components.Identity.MessageServices.Interfaces;
using malone.Core.EL.Identity;
using System;
using System.Threading.Tasks;

namespace malone.Core.BL.Components.Identity.MessageServices.Implementations
{

    public class SmsService : ISmsMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }
    }

}
