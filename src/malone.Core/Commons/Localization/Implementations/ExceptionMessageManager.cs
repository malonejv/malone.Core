using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.CL.Exceptions.Manager.Interfaces;

namespace malone.Core.CL.Exceptions.Manager.Implementations
{
    public class ExceptionMessageManager : IExceptionMessageManager
    {
        public string GetDescription(int code)
        {
            string errorKey = ((CoreErrors)code).ToString();
            var message = Resources.Exceptions.ResourceManager.GetString(errorKey);

            return message;
        }
    }
}
