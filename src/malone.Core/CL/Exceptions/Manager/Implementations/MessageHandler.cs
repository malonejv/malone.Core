using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using malone.Core.CL.Exceptions.Manager.Interfaces;

namespace malone.Core.CL.Exceptions.Manager.Implementations
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
