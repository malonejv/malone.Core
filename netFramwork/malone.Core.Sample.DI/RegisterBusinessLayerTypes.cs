using malone.Core.BL.Components.Implementations;
using malone.Core.BL.Components.Interfaces;
using malone.Core.Identity.BL.Components.MessageServices;
using malone.Core.Identity.BL.Components.MessageServices.Implementations;
using malone.Core.Identity.BL.Components.MessageServices.Interfaces;
using malone.Core.Identity.EntityFramework;
using malone.Core.Identity.EntityFramework.EL;
using malone.Core.Sample.Middle.BL;
using malone.Core.Sample.Middle.BL.Implementations;
using malone.Core.Sample.Middle.EL.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using Unity;

namespace malone.core.Sample.DI
{
    public static class RegisterBusinessLayerTypes
    {
        public static IUnityContainer RegisterTypes(IUnityContainer container)
        {
            //BUSINESS VALIDATORS
            container.RegisterType<ITodoListBV, TodoListBV>();
            container.RegisterType<IBusinessValidator<TaskItem>, BusinessValidator<TaskItem>>();

            //BUSINESS COMPONENTS
            container.RegisterType<ITodoListBC, TodoListBC>();
            container.RegisterType<ITaskItemBC, TaskItemBC>();

            //IDENTITY SERVICES
            container.RegisterType<UserManager<CoreUser, int>, UserBusinessComponent>();
            container.RegisterType<RoleManager<CoreRole, int>, RoleBusinessComponent>();
            container.RegisterType<SignInManager<CoreUser, int>, SignInBusinessComponent>();
            container.RegisterType<IEmailMessageService, EmailService>();
            container.RegisterType<ISmsMessageService, SmsService>();


            return container;
        }
    }
}
