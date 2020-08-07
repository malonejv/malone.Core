using malone.Core.Business.Components;
using malone.Core.Commons.Exceptions.Manager;
using malone.Core.Commons.Log;
using System;
using System.Reflection;

namespace malone.Core.Commons.Exceptions.Handler
{
    public class ExceptionHandler<TErrorCoder> : IExceptionHandler<TErrorCoder>
        where TErrorCoder : Enum
    {
        internal protected ILogger Logger { get; }

        internal protected IMessageHandler<TErrorCoder> MessageHandler { get; }

        public ExceptionHandler(ILogger logger, IMessageHandler<TErrorCoder> messageHandler)
        {
            Logger = logger;
            MessageHandler = messageHandler;
        }

        public void HandleException(Exception ex)
        {
            if (Logger != null)
                Logger.Error(ex);

            if (typeof(BaseException).IsAssignableFrom(ex.GetType()))
            {
                var baseException = ex as BaseException;

                if (baseException.Rethrow)
                    throw ex;
            }
        }

        public void HandleException(Exception ex, TErrorCoder errorCode, params object[] args)
        {
            string message = MessageHandler.GetMessage(errorCode, args);

            // Find the class
            Type exceptionType = typeof(TException);

            // Get it's constructor
            ConstructorInfo constructor = exceptionType.GetConstructor(new Type[] { typeof(TErrorCoder), typeof(string), typeof(Exception) });

            // Invoke it's constructor, which returns an instance.
            object[] constructorParams = { errorCode, message, ex };

            TException baseException = (TException)constructor.Invoke(constructorParams);

            if (Logger != null && baseException.ShouldLog)
                Logger.Error(ex);
            if (baseException.Rethrow)
                throw ex;
        }
    }
}
