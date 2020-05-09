using malone.Core.DAL.AdoNet.Attributes;
using malone.Core.DAL.AdoNet.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.DAL.AdoNet.Extensions
{
    public static class DbParameterAttributeExtensions
    {
        public static object Convert(this DbParameterAttribute parameter, object value)
        {
            object result;
            if (parameter.ValueConverter == null || parameter.ValueConverter.GetInterface(typeof(IParameterConverter).Name) == null)
            {
                result = value;
            }
            else
            {
                IParameterConverter parameterConverter = (IParameterConverter)Activator.CreateInstance(parameter.ValueConverter);
                result = parameterConverter.Convert(value);
            }
            return result;
        }
    }
}
