using malone.Core.BL.Components.Implementations;
using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Initializers;
using malone.Core.Identity.BL.Components.MessageServices;
using malone.Core.Identity.EntityFramework;
using malone.Core.Identity.EntityFramework.BL;
using malone.Core.Identity.EntityFramework.EL;
using malone.Core.Sample.Middle.BL;
using malone.Core.Sample.Middle.BL.Implementations;
using malone.Core.Sample.Middle.CL.Exceptions;
using malone.Core.Sample.Middle.EL.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Unity;

namespace malone.Core.Sample.DI
{
    public class BusinessLayerInitializer : ILayer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //BUSINESS VALIDATORS
            container.RegisterType<ITodoListBV, TodoListBV>();
            container.RegisterType<IBusinessValidator<TaskItem, ErrorCodes>, BusinessValidator<TaskItem, ErrorCodes>>();

            //BUSINESS COMPONENTS
            container.RegisterType<ITodoListBC, TodoListBC>();
            container.RegisterType<ITaskItemBC, TaskItemBC>();

            //IDENTITY SERVICES
            container.RegisterType<UserManager<CoreUser, int>, UserBusinessComponent>();
            container.RegisterType<RoleManager<CoreRole, int>, RoleBusinessComponent>();
            container.RegisterType<SignInManager<CoreUser, int>, SignInBusinessComponent>();
            container.RegisterType<IEmailMessageService, EmailService>();
            container.RegisterType<ISmsMessageService, SmsService>();

            container.RegisterType<IIdentityValidator<CoreUser>, UserValidator<CoreUser, int>>();
            container.RegisterType<IIdentityValidator<string>, PasswordValidator>();
            container.RegisterType<IUserManagerConfiguration, UserManagerConfiguration>();

        }
    }
}
