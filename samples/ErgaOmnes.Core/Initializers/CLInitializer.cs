using ErgaOmnes.Core.CL.Exceptions;
using malone.Core.Commons.Exceptions.Handler;
using malone.Core.Commons.Exceptions.Manager;
using malone.Core.Commons.Initializers;
using Unity;

namespace ErgaOmnes.Core.Initializers
{
    public class CLInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<IMessageHandler<ErrorCodes>, MessageHandler<ErrorCodes>>();
            container.RegisterType<IExceptionHandler<ErrorCodes>, ExceptionHandler<ErrorCodes>>();


        }
    }
}
