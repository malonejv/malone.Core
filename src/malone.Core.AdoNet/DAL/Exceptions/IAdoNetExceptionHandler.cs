using malone.Core.CL.Exceptions.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.AdoNet.DAL.Exceptions
{
    internal interface IAdoNetExceptionHandler : IExceptionHandler<AdoNetErrors>
    {
    }
}
