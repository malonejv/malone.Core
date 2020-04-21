using Unity;
using malone.Core.Sample.Middle.EL;
using malone.Core.Sample.Middle.EL.Model;
using malone.Core.Identity.EntityFramework.EL;
using Microsoft.AspNet.Identity;
using malone.Core.Identity.BL.Components.MessageServices;
using malone.Core.Identity.BL.Components.MessageServices.Interfaces;
using malone.Core.Identity.BL.Components.MessageServices.Implementations;

namespace malone.core.Sample.DI
{

    public static class RegisterEntitesLayer
    {
        public static IUnityContainer RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<CoreUser>();
            container.RegisterType<CoreRole>();
            container.RegisterType<CoreUserLogin>();
            container.RegisterType<CoreUserRole>();
            container.RegisterType<CoreUserClaim>();
            container.RegisterType<TodoList>();
            container.RegisterType<IEmailMessageService, EmailService>();
            container.RegisterType<ISmsMessageService, SmsService>();

            container.RegisterType<TodoList>();
            container.RegisterType<TaskItem>();

            return container;
        }
    }
}
