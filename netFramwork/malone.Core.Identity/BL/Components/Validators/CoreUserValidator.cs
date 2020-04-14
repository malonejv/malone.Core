using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Identity.EL;

namespace malone.Core.Identity.BL.Components.Validators
{
    public class CoreUserValidator<TUserEntity> : UserValidator<TUserEntity, int>
         where TUserEntity : CoreUser
    {
        public CoreUserValidator(UserManager<TUserEntity, int> manager) : base(manager) { }

    }
}
