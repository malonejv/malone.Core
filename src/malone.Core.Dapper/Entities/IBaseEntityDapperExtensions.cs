namespace malone.Core.Dapper.Entities
{
    public static class IBaseEntityDapperExtensions
    {
        //public static IEnumerable<ColumnAttribute> GetColumnsInfo<TKey, TEntity>(this TEntity entity)
        //    where TKey : IEquatable<TKey>
        //    where TEntity : class, IBaseEntity<TKey>
        //{
        //    List<ColumnAttribute> columns = new List<ColumnAttribute>();

        //    var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    foreach (var propertyInfo in properties)
        //    {
        //        ColumnAttribute columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();

        //        if (columnAttribute != null)
        //        {
        //            if (columnAttribute.Name.IsNullOrEmpty())
        //                columnAttribute.Name = propertyInfo.Name;

        //            object parameterValue = propertyInfo.GetValue(entity) != propertyInfo.GetType().GetDefault() ? propertyInfo.GetValue(entity) : DBNull.Value;
        //            columnAttribute.Value = parameterValue;

        //            columnAttribute.IsKey = propertyInfo.PropertyType.IsGenericParameter;
        //        }
        //        else
        //        {
        //            columnAttribute = new ColumnAttribute();

        //            columnAttribute.Name = propertyInfo.Name;

        //            object parameterValue = propertyInfo.GetValue(entity) != propertyInfo.GetType().GetDefault() ? propertyInfo.GetValue(entity) : DBNull.Value;
        //            columnAttribute.Value = parameterValue;
        //        }
        //    }

        //    return columns;
        //}

        //public static ColumnAttribute GetKeyColumnInfo<TKey, TEntity>(this TEntity entity)
        //    where TKey : IEquatable<TKey>
        //    where TEntity : class, IBaseEntity<TKey>
        //{
        //    ColumnAttribute columnAttribute = null;

        //    var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    foreach (var propertyInfo in properties)
        //    {
        //        bool isKey = propertyInfo.PropertyType.IsGenericParameter;

        //        if (isKey)
        //        {
        //            columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();

        //            if (columnAttribute != null)
        //            {
        //                if (columnAttribute.Name.IsNullOrEmpty())
        //                    columnAttribute.Name = propertyInfo.Name;

        //                object parameterValue = propertyInfo.GetValue(entity) != propertyInfo.GetType().GetDefault() ? propertyInfo.GetValue(entity) : DBNull.Value;
        //                columnAttribute.Value = parameterValue;

        //                columnAttribute.IsKey = isKey;
        //            }
        //            else
        //            {
        //                columnAttribute = new ColumnAttribute();

        //                columnAttribute.Name = propertyInfo.Name;

        //                object parameterValue = propertyInfo.GetValue(entity) != propertyInfo.GetType().GetDefault() ? propertyInfo.GetValue(entity) : DBNull.Value;
        //                columnAttribute.Value = parameterValue;
        //            }
        //        }
        //    }

        //    return columnAttribute;
        //}

        //public static string GetColumnNames<TKey, TEntity>(this TEntity entity)
        //    where TKey : IEquatable<TKey>
        //    where TEntity : class, IBaseEntity<TKey>
        //{
        //    var typeInfo = typeof(TEntity);

        //    List<string> columnNames = new List<string>();
        //    PropertyInfo[] properties = typeInfo.GetProperties(BindingFlags.Public);
        //    foreach (var prop in properties)
        //    {
        //        ColumnAttribute columnAttribute = prop.GetCustomAttribute<ColumnAttribute>();
        //        if (columnAttribute != null)
        //            columnNames.Add(columnAttribute.Name);
        //        else
        //            columnNames.Add(prop.Name);
        //    }

        //    return columnNames.Aggregate((i, j) => $"{i}, {j}");
        //}

        //public static string GetTableName<TKey, TEntity>(this TEntity entity)
        //    where TKey : IEquatable<TKey>
        //    where TEntity : class, IBaseEntity<TKey>
        //{
        //    var typeInfo = typeof(TEntity);

        //    string tableName = typeInfo.Name;

        //    TableAttribute tableAttribute = typeof(TEntity).GetCustomAttribute<TableAttribute>();
        //    if (tableAttribute != null)
        //        tableName = tableAttribute.Name;

        //    return tableName;
        //}

    }
}
