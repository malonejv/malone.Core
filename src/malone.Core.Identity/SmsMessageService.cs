using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

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
