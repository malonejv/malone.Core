using System;
using System.Collections.Generic;
using System.Text;

namespace malone.Core.CL.Configurations
{
    public interface ICoreConfiguration
    {
        T GetSection<T>(string sectionName);
    }
}
