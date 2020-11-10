using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using malone.Core.AdoNet.Attributes;
using malone.Core.AdoNet.Database;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Entities.Model;

namespace malone.Core.AdoNet.Entities
{
    public static class IBaseEntityAdoNetExtensions
    {
        private static readonly string ID = "Id";

        public static IEnumerable<DbParameterWithValue> GetNotKeyParameters<TKey, TEntity>(this TEntity entity)
            where TKey : IEquatable<TKey>
            where TEntity : class, IBaseEntity<TKey>
        {
            var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.Name != ID)
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

        public static DbParameterWithValue GetKeyParameter<TKey>(this Type entityType, TKey id) where TKey : IEquatable<TKey>
        {
            if (typeof(IBaseEntity<TKey>).IsAssignableFrom(entityType))
            {
                var interfaceType = entityType.GetInterface(typeof(IBaseEntity<TKey>).Name);

                var targetMethods = from method in entityType.GetInterfaceMap(interfaceType).TargetMethods
                                    select method;

                var propertyInfoId = (from prop in entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                      where (targetMethods.Contains(prop.GetGetMethod(true)) || targetMethods.Contains(prop.GetSetMethod(true)))
                                         && prop.Name == ID
                                      select prop).Single();

                DbParameterAttribute dbParameterInfo = propertyInfoId.GetCustomAttribute(typeof(DbParameterAttribute), false) as DbParameterAttribute;

                // Get the stringvalue attributes
                //DbParameterIdAttribute dbParameterInfo = entityType.GetCustomAttribute(typeof(DbParameterIdAttribute), false) as DbParameterIdAttribute;
                return new DbParameterWithValue
                {
                    DbParameter = dbParameterInfo,
                    Value = id
                };
            }
            //TODO: manejar con excepciones de core.
            throw new InvalidOperationException("Se esperaba IBaseEntity<TKey>");
        }
    }
}
