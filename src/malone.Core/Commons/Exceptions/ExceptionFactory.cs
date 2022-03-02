//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:54</date>

using System;
using System.Reflection;
using malone.Core.IoC;
using malone.Core.Localization;

namespace malone.Core.Commons.Exceptions
	{
	public static class ExceptionFactory<TCode, TErrorLocalizationHandler>
        where TCode : Enum
        where TErrorLocalizationHandler : ILocalizationHandler<TCode>
    {
        internal static TErrorLocalizationHandler errorLocalizationHandler;

        internal static TErrorLocalizationHandler ErrorLocalizationHandler
        {
            get
            {
                if (errorLocalizationHandler == null)
                {
                    errorLocalizationHandler = ServiceLocator.Current.Get<TErrorLocalizationHandler>();
                }
                return errorLocalizationHandler;
            }
        }

        public static TException CreateException<TException>(TCode code, params object[] args) where TException : BaseException
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

        public static TException CreateException<TException>(Exception innerException, TCode code, params object[] args) where TException : BaseException
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
