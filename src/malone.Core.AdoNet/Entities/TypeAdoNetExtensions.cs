using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using malone.Core.AdoNet.Attributes;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Entities.Model;

namespace malone.Core.AdoNet.Entities
{
	internal static class TypeAdoNetExtensions
	{
		public static IEnumerable<ColumnAttribute> GetColumnsInfo(this Type entityType)
		{
			List<ColumnAttribute> columns = new List<ColumnAttribute>();

			var properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (var propertyInfo in properties)
			{
				ColumnAttribute columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();

				if (columnAttribute != null)
				{
					if (columnAttribute.Name.IsNullOrEmpty())
					{
						columnAttribute.Name = propertyInfo.Name;
					}
				}
				else
				{
					columnAttribute = new ColumnAttribute();

					columnAttribute.Name = propertyInfo.Name;
				}

				object parameterValue = propertyInfo.GetValue(entityType) != propertyInfo.GetType().GetDefault() ? propertyInfo.GetValue(entityType) : DBNull.Value;
				columnAttribute.Value = parameterValue;

				columnAttribute.PropertyName = propertyInfo.Name;
				columnAttribute.PropertyType = propertyInfo.PropertyType;

				columns.Add(columnAttribute);
			}

			return columns;
		}

		public static ColumnAttribute GetColumnInfo(this Type entityType, string propertyName)
		{
			ColumnAttribute columnAttribute;

			PropertyInfo propertyInfo = entityType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

			if (propertyInfo != null)
			{
				columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();

				if (columnAttribute != null)
				{
					if (columnAttribute.Name.IsNullOrEmpty())
					{
						columnAttribute.Name = propertyInfo.Name;
					}
				}
				else
				{
					columnAttribute = new ColumnAttribute();

					columnAttribute.Name = propertyInfo.Name;
				}

				object parameterValue = propertyInfo.GetValue(entityType) != propertyInfo.GetType().GetDefault() ? propertyInfo.GetValue(entityType) : DBNull.Value;
				columnAttribute.Value = parameterValue;

				columnAttribute.PropertyName = propertyInfo.Name;
				columnAttribute.PropertyType = propertyInfo.PropertyType;

			}
			else
				throw new Exception("Not a valid property name.");

			return columnAttribute;
		}

		public static List<string> GetColumnNames(this Type entityType)
		{
			List<string> columnNames = new List<string>();
			PropertyInfo[] properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (var prop in properties)
			{
				ColumnAttribute columnAttribute = prop.GetCustomAttribute<ColumnAttribute>();
				if (columnAttribute != null)
				{
					columnNames.Add(columnAttribute.Name);
				}
				else if (prop.PropertyType.IsPrimitive
					 || (prop.PropertyType.IsClass && prop.PropertyType.Name == "String")
					 || (prop.PropertyType.IsGenericType && prop.PropertyType.Name == "Nullable`1"))
				{
					columnNames.Add(prop.Name);
				}
			}

			return columnNames;
		}

		public static string GetColumnName(this Type entityType, string propertyName)
		{
			string columnName = propertyName;

			PropertyInfo propertyInfo = entityType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

			if (propertyInfo != null)
			{
				ColumnAttribute columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();
				if (columnAttribute != null)
				{
					columnName = columnAttribute.Name;
				}
				else if (propertyInfo.PropertyType.IsPrimitive
					 || (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType.Name == "String")
					 || (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.Name == "Nullable`1"))
				{
					columnName = propertyInfo.Name;
				}
			}
			else
				throw new Exception("Not a valid property name.");
			//TODO: Manejar con excepciones del Core

			return columnName;
		}

		public static ColumnAttribute GetKeyColumnInfo(this Type entityType)
		{
			ColumnAttribute columnAttribute = null;

			string entityName = entityType.Name;
			bool isBaseEntity = entityType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseEntity<>));

			var properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (var propertyInfo in properties)
			{
				/*if (isBaseEntity)
				{
					bool isKey = propertyInfo.PropertyType.IsGenericParameter;

					if (isKey)
					{
						columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();

						if (columnAttribute != null)
						{
							if (columnAttribute.Name.IsNullOrEmpty())
								columnAttribute.Name = propertyInfo.Name;

							object parameterValue = propertyInfo.GetValue(entityType) != propertyInfo.GetType().GetDefault() ? propertyInfo.GetValue(entityType) : DBNull.Value;
							columnAttribute.Value = parameterValue;
						}
						else
						{
							columnAttribute = new ColumnAttribute();

							columnAttribute.Name = propertyInfo.Name;

							object parameterValue = propertyInfo.GetValue(entityType) != propertyInfo.GetType().GetDefault() ? propertyInfo.GetValue(entityType) : DBNull.Value;
							columnAttribute.Value = parameterValue;
						}
						columnAttribute.IsKey = isKey;
					}
				}
				else*/
				if (propertyInfo.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase) ||
				  propertyInfo.Name.RemoveSpecialCharacters().Equals($"{entityName}id", StringComparison.CurrentCultureIgnoreCase) ||
				  propertyInfo.Name.RemoveSpecialCharacters().Equals($"id{entityName}", StringComparison.CurrentCultureIgnoreCase))
				{
					columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();
					if (columnAttribute == null)
					{
						columnAttribute = new ColumnAttribute(name: propertyInfo.Name, direction: ParameterDirection.Input, isKey: true);
					}

					break;
				}
			}


			if (columnAttribute == null)
			{
				//TODO: Manejar con excepciones del Core
				throw new Exception("Key field not found.");
			}

			return columnAttribute;
		}

		public static string GetTableName(this Type entityType)
		{
			string tableName = entityType.Name;

			TableAttribute tableAttribute = entityType.GetCustomAttribute<TableAttribute>();
			if (tableAttribute != null)
			{
				tableName = tableAttribute.Name;
			}

			return tableName;
		}

	}
}
