using malone.Core.EL.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace malone.Core.BL.Components.Identity.Providers
{
    public class CoreDataProtectorTokenProvider : DataProtectorTokenProvider<CoreUser>
    {
        public CoreDataProtectorTokenProvider(IDataProtector protector, IOptions<DataProtectionTokenProviderOptions> options) : base(protector,options)
        {
        }
    }
}
