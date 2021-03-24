using malone.Core.AdoNet.Parameters;
using System;

namespace malone.Core.AdoNet.Attributes
{
    public static class DbFieldAttributeExtensions
    {
        public static object Convert(this DbFieldAttribute parameter, object value)
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
