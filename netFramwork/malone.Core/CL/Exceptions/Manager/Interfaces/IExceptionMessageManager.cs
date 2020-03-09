using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions.Manager.Interfaces
{
    public interface IExceptionMessageManager
    {
        string GetDescription(int code);
    }
}
