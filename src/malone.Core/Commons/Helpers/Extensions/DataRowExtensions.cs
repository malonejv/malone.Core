using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Helpers.Extensions
{
    public static class DataRowExtensions
    {

        public static bool IsNull(this DataRow row)
        {
            return row is null;
        }

        public static int? AsInt(this DataRow row, string columnName)
        {
            if (row.IsNull())
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(row));
            }

            if (string.IsNullOrEmpty(columnName) || !row.Table.Columns.Contains(columnName))
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(columnName));
            }

            if (row[columnName] is DBNull)
                return null;

            return Convert.ToInt32(row[columnName]);
        }

        public static int AsIntOrDefault(this DataRow row, string columnName)
        {
            if (row.IsNull())
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(row));
            }

            if (string.IsNullOrEmpty(columnName) || !row.Table.Columns.Contains(columnName))
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(columnName));
            }

            if (row[columnName] is DBNull)
                return default(int);

            return Convert.ToInt32(row[columnName]);
        }

        public static decimal? AsDecimal(this DataRow row, string columnName)
        {
            if (row.IsNull())
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(row));
            }

            if (string.IsNullOrEmpty(columnName) || !row.Table.Columns.Contains(columnName))
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(columnName));
            }

            if (row[columnName] is DBNull)
                return null;

            return Convert.ToDecimal(row[columnName]);
        }

        public static decimal AsDecimalOrDefault(this DataRow row, string columnName)
        {
            if (row.IsNull())
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(row));
            }

            if (string.IsNullOrEmpty(columnName) || !row.Table.Columns.Contains(columnName))
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(columnName));
            }

            if (row[columnName] is DBNull)
                return default(decimal);

            return Convert.ToDecimal(row[columnName]);
        }

        public static bool? AsBoolean(this DataRow row, string columnName)
        {
            if (row.IsNull())
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(row));
            }

            if (string.IsNullOrEmpty(columnName) || !row.Table.Columns.Contains(columnName))
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(columnName));
            }

            if (row[columnName] is DBNull)
                return null;

            return Convert.ToBoolean(row[columnName]);
        }

        public static bool AsBooleanOrDefault(this DataRow row, string columnName)
        {
            if (row.IsNull())
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(row));
            }

            if (string.IsNullOrEmpty(columnName) || !row.Table.Columns.Contains(columnName))
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(columnName));
            }

            if (row[columnName] is DBNull)
                return default(bool);

            return Convert.ToBoolean(row[columnName]);
        }

        public static DateTime? AsDate(this DataRow row, string columnName)
        {
            if (row.IsNull())
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(row));
            }

            if (string.IsNullOrEmpty(columnName) || !row.Table.Columns.Contains(columnName))
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(columnName));
            }

            if (row[columnName] is DBNull)
                return null;

            return Convert.ToDateTime(row[columnName]);
        }

        public static DateTime AsDateOrDefault(this DataRow row, string columnName)
        {
            if (row.IsNull())
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(row));
            }

            if (string.IsNullOrEmpty(columnName) || !row.Table.Columns.Contains(columnName))
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(columnName));
            }

            if (row[columnName] is DBNull)
                return default(DateTime);

            return Convert.ToDateTime(row[columnName]);
        }

        public static string AsString(this DataRow row, string columnName)
        {
            if (row.IsNull())
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(row));
            }

            if (string.IsNullOrEmpty(columnName) || !row.Table.Columns.Contains(columnName))
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(columnName));
            }

            if (row[columnName] is DBNull)
                return null;

            return row[columnName].ToString();
        }

        public static T AsTOrDefault<T>(this DataRow row, string columnName)
            where T: IEquatable<T>
        {
            if (row.IsNull())
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(row));
            }

            if (string.IsNullOrEmpty(columnName) || !row.Table.Columns.Contains(columnName))
            {//TODO: Manejar con excepciones del Core
                throw new ArgumentException(nameof(columnName));
            }

            if (row[columnName] is DBNull)
                return default(T);

            return ChangeType<T>(row[columnName].ToString());
        }

        private static T ChangeType<T>(string value)
        {
            var t = typeof(T);
            if (t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                t = Nullable.GetUnderlyingType(t);
            }

            return (T)Convert.ChangeType(value, t);
        }
    }
}
