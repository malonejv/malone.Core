using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Exceptions.Manager
{
    public interface IMessageHandler<TCode>
        where TCode : Enum
    {
        string GetMessage(TCode code, params object[] args);
    }
}
