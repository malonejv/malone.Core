using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.EL.Identity;

namespace malone.Core.BL.Components.Identity.Validators
{
    public class CoreUserValidator<TUserEntity> : UserValidator<TUserEntity, int>
         where TUserEntity : CoreUser
    {
        public CoreUserValidator(UserManager<TUserEntity, int> manager) : base(manager) { }

    }
}
