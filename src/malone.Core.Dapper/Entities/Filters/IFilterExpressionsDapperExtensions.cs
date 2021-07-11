using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Dapper.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace malone.Core.Dapper.Entities.Filters
{
    public static class IFilterExpressionDapperExtensions
    {
        public static List<ParameterAttribute> GetParameters(this IFilterExpressionDapper filter)
        {
            List<ParameterAttribute> parameters = new List<ParameterAttribute>();

            var properties = filter.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in properties)
            {
                ParameterAttribute parameterAttribute = propertyInfo.GetCustomAttribute<ParameterAttribute>();

                if (parameterAttribute != null)
                {
                    if (parameterAttribute.Name.IsNullOrEmpty())
                        parameterAttribute.Name = propertyInfo.Name;
                }
                else
                {
                    parameterAttribute = new ParameterAttribute();
                    parameterAttribute.Name = propertyInfo.Name;
                }

                object parameterValue = propertyInfo.GetValue(filter) != propertyInfo.GetType().GetDefault() ? propertyInfo.GetValue(filter) : DBNull.Value;
                parameterAttribute.Value = parameterValue;

                if (!parameterAttribute.IgnoreDbNull || parameterAttribute.Value != DBNull.Value)
                    parameters.Add(parameterAttribute);
            }

            return parameters;
        }


    }
}
