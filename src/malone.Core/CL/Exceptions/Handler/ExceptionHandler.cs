using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.CL.Exceptions.Manager.Interfaces;
using malone.Core.CL.Log;

namespace malone.Core.CL.Exceptions.Handler
{
    public class ExceptionHandler<TCode> : IExceptionHandler<TCode>
        where TCode : Enum
    {
        internal protected ILogger Logger { get; }
        internal protected IMessageHandler<TCode> MessageHandler { get; }

        public ExceptionHandler(ILogger logger, IMessageHandler<TCode> messageHandler)
        {
            Logger = logger;
            MessageHandler = messageHandler;
        }

        public void HandleException(Exception ex)
        {
            if (Logger != null)
                Logger.Error(ex);
        }
        public void HandleException<TException>(TCode errorCode, params object[] args)
            where TException : BaseException<TCode>
        {
            string message = MessageHandler.GetMessage(errorCode, args);

            // Find the class
            Type exceptionType = typeof(TException);

            // Get it's constructor
            ConstructorInfo constructor = exceptionType.GetConstructor(new Type[] { });

            // Invoke it's constructor, which returns an instance.
            object[] constructorParams = { errorCode, message };

            TException baseException = (TException)constructor.Invoke(constructorParams);

            if (Logger != null && baseException.ShouldLog)
                Logger.Error(baseException);
            if (baseException.Rethrow)
                throw baseException;
        }

        public void HandleException<TException>(Exception ex, TCode errorCode, params object[] args)
            where TException : BaseException<TCode>
        {
            string message = MessageHandler.GetMessage(errorCode, args);

            // Find the class
            Type exceptionType = typeof(TException);

            // Get it's constructor
            ConstructorInfo constructor = exceptionType.GetConstructor(new Type[] { });

            // Invoke it's constructor, which returns an instance.
            object[] constructorParams = { errorCode, message, ex };

            TException baseException = (TException)constructor.Invoke(constructorParams);

            if (Logger != null && baseException.ShouldLog)
                Logger.Error(ex);
            if (baseException.Rethrow)
                throw ex;
        }

        public void HandleException<TValidation>(ValidationResultList validationResult) where TValidation : BusinessValidationException<TCode>
        {
            // Find the class
            Type exceptionType = typeof(TValidation);

            // Get it's constructor
            ConstructorInfo constructor = exceptionType.GetConstructor(new Type[] { });

            // Invoke it's constructor, which returns an instance.
            object[] constructorParams = { validationResult };

            TValidation baseException = (TValidation)constructor.Invoke(constructorParams);

            if (Logger != null && baseException.ShouldLog)
                Logger.Error(baseException);
            if (baseException.Rethrow)
                throw baseException;
        }
    }
}
