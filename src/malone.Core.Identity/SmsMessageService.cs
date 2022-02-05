using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace malone.Core.Identity
{

    public class SmsService : ISmsMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }
    }

}
