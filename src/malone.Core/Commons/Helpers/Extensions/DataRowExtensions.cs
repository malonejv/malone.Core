//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:57</date>

using System;
using System.Data;
using System.Reflection;

namespace malone.Core.Commons.Helpers.Extensions
{
    /// <summary>
    /// Defines the <see cref="DataRowExtensions" />.
    /// </summary>
    public static class DataRowExtensions
    {
        /// <summary>
        /// The IsNull.
        /// </summary>
        /// <param name="row">The row<see cref="DataRow"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsNull(this DataRow row)
        {
            return row is null;
        }

        /// <summary>
        /// The AsInt.
        /// </summary>
        /// <param name="row">The row<see cref="DataRow"/>.</param>
        /// <param name="columnName">The columnName<see cref="string"/>.</param>
        /// <returns>The <see cref="int?"/>.</returns>
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

        /// <summary>
        /// The AsIntOrDefault.
        /// </summary>
        /// <param name="row">The row<see cref="DataRow"/>.</param>
        /// <param name="columnName">The columnName<see cref="string"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
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

        /// <summary>
        /// The AsDecimal.
        /// </summary>
        /// <param name="row">The row<see cref="DataRow"/>.</param>
        /// <param name="columnName">The columnName<see cref="string"/>.</param>
        /// <returns>The <see cref="decimal?"/>.</returns>
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

        /// <summary>
        /// The AsDecimalOrDefault.
        /// </summary>
        /// <param name="row">The row<see cref="DataRow"/>.</param>
        /// <param name="columnName">The columnName<see cref="string"/>.</param>
        /// <returns>The <see cref="decimal"/>.</returns>
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

        /// <summary>
        /// The AsBoolean.
        /// </summary>
        /// <param name="row">The row<see cref="DataRow"/>.</param>
        /// <param name="columnName">The columnName<see cref="string"/>.</param>
        /// <returns>The <see cref="bool?"/>.</returns>
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

        /// <summary>
        /// The AsBooleanOrDefault.
        /// </summary>
        /// <param name="row">The row<see cref="DataRow"/>.</param>
        /// <param name="columnName">The columnName<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
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

        /// <summary>
        /// The AsDate.
        /// </summary>
        /// <param name="row">The row<see cref="DataRow"/>.</param>
        /// <param name="columnName">The columnName<see cref="string"/>.</param>
        /// <returns>The <see cref="DateTime?"/>.</returns>
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

        /// <summary>
        /// The AsDateOrDefault.
        /// </summary>
        /// <param name="row">The row<see cref="DataRow"/>.</param>
        /// <param name="columnName">The columnName<see cref="string"/>.</param>
        /// <returns>The <see cref="DateTime"/>.</returns>
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

        /// <summary>
        /// The AsString.
        /// </summary>
        /// <param name="row">The row<see cref="DataRow"/>.</param>
        /// <param name="columnName">The columnName<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
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

        /// <summary>
        /// The AsTOrDefault.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="row">The row<see cref="DataRow"/>.</param>
        /// <param name="columnName">The columnName<see cref="string"/>.</param>
        /// <returns>The <see cref="T"/>.</returns>
        public static T AsTOrDefault<T>(this DataRow row, string columnName)
            where T : IEquatable<T>
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

        /// <summary>
        /// The ChangeType.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="value">The value<see cref="string"/>.</param>
        /// <returns>The <see cref="T"/>.</returns>
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
