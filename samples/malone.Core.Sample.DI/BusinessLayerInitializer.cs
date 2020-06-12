﻿using malone.Core.Business.Components;
using malone.Core.Commons.Initializers;
using malone.Core.Identity.EntityFramework;
using malone.Core.Sample.Middle.BL;
using malone.Core.Sample.Middle.BL.Implementations;
using malone.Core.Sample.Middle.EL.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Unity;

namespace malone.Core.Sample.DI
{
    public class BusinessLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //BUSINESS VALIDATORS
            container.RegisterType<ITodoListBV, TodoListBV>();
            container.RegisterType<IBusinessValidator<TaskItem>, BusinessValidator<TaskItem>>();

            //BUSINESS COMPONENTS
            container.RegisterType<ITodoListBC, TodoListBC>();
            container.RegisterType<ITaskItemBC, TaskItemBC>();

            ////IDENTITY SERVICES
            //container.RegisterType<UserManager<CoreUser, int>, UserBusinessComponent>();
            //container.RegisterType<RoleManager<CoreRole, int>, RoleBusinessComponent>();
            //container.RegisterType<SignInManager<CoreUser, int>, SignInBusinessComponent>();
            //container.RegisterType<IEmailMessageService, EmailService>();
            //container.RegisterType<ISmsMessageService, SmsService>();

            //container.RegisterType<IIdentityValidator<CoreUser>, UserValidator<CoreUser, int>>();
            //container.RegisterType<IIdentityValidator<string>, PasswordValidator>();
            //container.RegisterType<IUserManagerConfiguration, UserManagerConfiguration>();

        }
    }
}