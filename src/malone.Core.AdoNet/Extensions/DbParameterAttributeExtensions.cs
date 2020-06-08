using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Parameters;
using System;

namespace malone.Core.AdoNet.Extensions
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
