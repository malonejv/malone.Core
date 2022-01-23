using Dapper;
using malone.Core.AdoNet.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Dapper.SqlServer
{
    public class SqlDatabase : IDatabase
    {
        private string ConnectionString { get; set; }


        public SqlDatabase(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }


        public void CloseConnection(IDbConnection connection)
        {
            var sqlconnection = (SqlConnection)connection;
            sqlconnection.Close();
            sqlconnection.Dispose();
        }

        public IDataAdapter CreateAdapter(IDbCommand command)
        {
            return new SqlDataAdapter((SqlCommand)command);
        }

        public IDbDataParameter CreateParameter(IDbCommand command)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            return sqlCommand.CreateParameter();
        }

        public void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType)
        {
            SqlCommand sqlCommand = SqlDatabase.ValidateCommand(command);
            if (parameterType is SqlDbType)
            {
                var dbType = (SqlDbType)parameterType;
                SqlParameter sqlParameter = sqlCommand.Parameters.Add(parameterName, dbType);
                sqlParameter.Value = value;
                sqlParameter.Direction = parameterdirection;
            }
            else
            {
                //TODO: manejar con errores del core.
                throw new InvalidOperationException(string.Format("SqlType unrecognized: {0}", (object)parameterName));
            }
        }

        public void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType, int size)
        {
            SqlCommand sqlCommand = SqlDatabase.ValidateCommand(command);
            if (parameterType is SqlDbType)
            {
                var dbType = (SqlDbType)parameterType;
                SqlParameter sqlParameter = sqlCommand.Parameters.Add(parameterName, dbType, size);
                sqlParameter.Value = value;
                sqlParameter.Direction = parameterdirection;
            }
            else
            {
                //TODO: manejar con errores del core.
                throw new InvalidOperationException(string.Format("SqlType unrecognized: {0}", (object)parameterName));
            }
        }

        private static SqlCommand ValidateCommand(IDbCommand command)
        {
            if (!(command is SqlCommand sqlCommand))
                throw new InvalidOperationException("Error");
            //TODO: manejar con errores del core.
            //string.Format((IFormatProvider)CultureInfo.CurrentCulture, Resources.SqlCommandExpected, (object)command.GetType().FullName))
            return sqlCommand;
        }
    }
}
