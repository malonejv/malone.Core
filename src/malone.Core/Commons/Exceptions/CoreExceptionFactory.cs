//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:53</date>

using malone.Core.Commons.DI;
using System;
using System.Reflection;

namespace malone.Core.Commons.Exceptions
{
                internal static class CoreExceptionFactory
    {
                                internal static IErrorLocalizationHandler errorLocalizationHandler;

                                internal static IErrorLocalizationHandler ErrorLocalizationHandler
        {
            get
            {
                if (errorLocalizationHandler == null)
                {
                    errorLocalizationHandler = ServiceLocator.Current.Get<IErrorLocalizationHandler>();
                }
                return errorLocalizationHandler;
            }
        }

                                                                internal static TException CreateException<TException>(CoreErrors code, params object[] args) where TException : BaseException
        {
            var suportId = Guid.NewGuid();
            string message = ErrorLocalizationHandler.GetString(code, args);

            // Find the class
            Type exceptionType = typeof(TException);

            // Get it's constructor
            ConstructorInfo constructor = exceptionType.GetConstructor(new Type[] { typeof(string) });

            // Invoke it's constructor, which returns an instance.
            object[] constructorParams = { message };

            TException baseException = (TException)constructor.Invoke(constructorParams);

            baseException.Data.Add(BaseException.SUPPORT_ID, suportId);
            baseException.Data.Add(BaseException.ERROR_CODE, code.ToString());

            return baseException;
        }

                                                                        internal static TException CreateException<TException>(Exception innerException, CoreErrors code, params object[] args) where TException : BaseException
        {
            var suportId = Guid.NewGuid();
            string message = ErrorLocalizationHandler.GetString(code, args);

            // Find the class
            Type exceptionType = typeof(TException);

            // Get it's constructor
            ConstructorInfo constructor = exceptionType.GetConstructor(new Type[] { typeof(string), typeof(Exception) });

            // Invoke it's constructor, which returns an instance.
            object[] constructorParams = { message, innerException };

            TException baseException = (TException)constructor.Invoke(constructorParams);

            baseException.Data.Add(BaseException.SUPPORT_ID, suportId);
            baseException.Data.Add(BaseException.ERROR_CODE, code.ToString());

            return baseException;
        }
    }
}
