using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Identity.EL;
using Microsoft.Owin.Security.DataProtection;

namespace malone.Core.Identity.BL.Components.Providers
{
    public class CoreDataProtectorTokenProvider<TUserEntity> : DataProtectorTokenProvider<TUserEntity, int>
        where TUserEntity : CoreUser
    {
        public CoreDataProtectorTokenProvider(IDataProtector protector) : base(protector)
        {
        }
    }
}
