using System;

namespace malone.Core.CL.Exceptions.Manager
{
    public class MessageHandler<TCode> : IMessageHandler<TCode>
        where TCode : Enum
    {
        public string GetMessage(TCode code, params object[] args)
        {
            string errorKey = ((TCode)code).ToString();
            var message = Resources.Exceptions.ResourceManager.GetString(errorKey);

            if (args != null && args.Length > 0)
            {
                message = string.Format(message, args);
            }

            return message;
        }
    }
}
