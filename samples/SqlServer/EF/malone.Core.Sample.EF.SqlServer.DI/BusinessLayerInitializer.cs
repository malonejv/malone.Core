using malone.Core.Services;
using malone.Core.Commons.Initializers;
using malone.Core.Sample.EF.SqlServer.Middle.BL;
using malone.Core.Sample.EF.SqlServer.Middle.BL.Implementations;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using Unity;

namespace malone.Core.Sample.EF.SqlServer.DI
{
    public class BusinessLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //BUSINESS VALIDATORS
            container.RegisterType<ITodoListBV, TodoListBV>();
            container.RegisterType<IServiceValidator<TaskItem>, ServiceValidator<TaskItem>>();

            //BUSINESS COMPONENTS
            container.RegisterType<ITodoListBC, TodoListBC>();
            container.RegisterType<ITaskItemBC, TaskItemBC>();

            ////IDENTITY SERVICES
            //container.RegisterType<UserManager<CoreUser, int>, UserService>();
            //container.RegisterType<RoleManager<CoreRole, int>, RoleService>();
            //container.RegisterType<SignInManager<CoreUser, int>, SignInService>();
            //container.RegisterType<IEmailMessageService, EmailService>();
            //container.RegisterType<ISmsMessageService, SmsService>();

            //container.RegisterType<IIdentityValidator<CoreUser>, UserValidator<CoreUser, int>>();
            //container.RegisterType<IIdentityValidator<string>, PasswordValidator>();
            //container.RegisterType<IUserManagerConfiguration, UserManagerConfiguration>();

        }
    }
}
