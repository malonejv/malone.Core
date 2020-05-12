using malone.Core.AdoNet.DAL.Attributes;
using malone.Core.AdoNet.DAL.Database;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace malone.Core.AdoNet.EL.Filters.Extensions
{
    public static class IFilterExpressionAdoNetExtensions
    {
        public static List<IDbDataParameter> SetConfiguredParameters(this IFilterExpressionAdoNet filter, IDbCommand command, IDatabase db)
        {
            var parameters = new List<IDbDataParameter>();

            var infoList = new Dictionary<PropertyInfo, DbParameterAttribute>();
            var properties = filter.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                // Get the stringvalue attributes
                DbParameterAttribute dbParameterInfo = prop.GetCustomAttribute(typeof(DbParameterAttribute), false) as DbParameterAttribute;
                infoList.Add(prop, dbParameterInfo);
            }

            foreach (var info in infoList.OrderBy(i => i.Value.Order))
            {
                var prop = info.Key;
                var dbParameterInfo = info.Value;

                var parameter = db.CreateParameter(command);
                parameter.ParameterName = string.Concat("@", !string.IsNullOrEmpty(dbParameterInfo.Name) ? dbParameterInfo.Name.First() == '@' ? dbParameterInfo.Name.Substring(1) : dbParameterInfo.Name : prop.Name);
                parameter.Value = (prop.GetValue(filter) != null) ? prop.GetValue(filter) : (dbParameterInfo.DefaultValue != null) ? dbParameterInfo.DefaultValue : null;
                if (dbParameterInfo.IsSizeDefined) parameter.Size = dbParameterInfo.Size;

                db.AddParameter(command, parameter, dbParameterInfo.Type);
            }

            return null;
        }
    }
}
