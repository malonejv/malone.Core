using System;
using System.Resources;
using malone.Core.Commons.DI;
using malone.Core.Commons.Helpers.Extensions;

namespace malone.Core.Commons.Localization
{
    public abstract class LocalizationHandler<TCode> : ILocalizationHandler<TCode>
        where TCode : Enum
    {
        public abstract ResourceManager ResourceManager { get; }

        public LocalizationHandler() { }

        public string GetString(TCode code, params object[] args)
        {
            string message = "";

            string errorKey = code.ToString();
            message = ResourceManager?.GetString(errorKey);

            if (message != null && args != null && args.Length > 0)
            {
                message = string.Format(message, args);
            }

            return message;
        }
    }
}
