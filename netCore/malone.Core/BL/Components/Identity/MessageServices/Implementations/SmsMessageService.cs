using malone.Core.BL.Components.Identity.MessageServices.Interfaces;
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
