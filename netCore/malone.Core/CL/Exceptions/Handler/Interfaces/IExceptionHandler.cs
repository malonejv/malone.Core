using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions.Handler.Interfaces
{
    public interface IExceptionHandler
    {
        void HandleException(Exception ex);
    }

}
