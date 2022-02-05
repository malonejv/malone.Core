using System;
using System.Collections.Generic;
using System.Reflection;
using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Database;
using malone.Core.Commons.Helpers.Extensions;

namespace malone.Core.AdoNet.Entities
{
    public static class EntityAdoNetExtensions
    {
        public static IEnumerable<DbParameterWithValue> GetParameters<TEntity>(this TEntity entity)
            where TEntity : class
        {
            var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in properties)
            {
                // Get the stringvalue attributes
                DbParameterAttribute dbParameterInfo = propertyInfo.GetCustomAttribute(typeof(DbParameterAttribute), false) as DbParameterAttribute;
                if (dbParameterInfo != null)
                {
                    object parameterValue = propertyInfo.GetValue(entity) != propertyInfo.GetType().GetDefault() ? propertyInfo.GetValue(entity) : DBNull.Value;
                    yield return new DbParameterWithValue
                    {
                        DbParameter = dbParameterInfo,
                        Value = parameterValue
                    };
                }
            }
        }

    }
}
