using malone.Core.BL.Components.Implementations;
using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.CL.Exceptions.Manager;
using malone.Core.Identity.EntityFramework.EL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Identity.EntityFramework.BL
{
    public class UserBussinessValidator<TKey, TUserEntity, TUserLogin, TUserRole, TUserClaim, TErrorCoder> : BusinessValidator<TKey, TUserEntity, TErrorCoder>
        where TKey : IEquatable<TKey>
        where TUserLogin : CoreUserLogin<TKey>, new()
        where TUserRole : CoreUserRole<TKey>, new()
        where TUserClaim : CoreUserClaim<TKey>, new()
        where TUserEntity : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TErrorCoder : Enum
    {

        public UserBussinessValidator(IMessageHandler<TErrorCoder> messageHandler, IExceptionHandler<TErrorCoder> exceptionHandler) : base(messageHandler, exceptionHandler)
        {
        }
    }
}
