using malone.Core.Commons.Initializers;
using malone.Core.Sample.AdoNet.SqlServer.Middle.BL;
using malone.Core.Sample.AdoNet.SqlServer.Middle.BL.Implementations;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;
using malone.Core.Services;
using Unity;

namespace malone.Core.Sample.AdoNet.SqlServer.DI
{
	public class BusinessLayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            //BUSINESS VALIDATORS
            container.RegisterType<ITodoListBV, TodoListBV>();
            container.RegisterType<ITaskItemBV, TaskItemBV>();

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
