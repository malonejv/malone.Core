using malone.Core.DAL.AdoNet.DatabasConnection;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace malone.Core.DAL.AdoNet.SqlClient
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

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            return new SqlCommand()
            {
                CommandText = commandText,
                CommandType = commandType,
                Connection = (SqlConnection)connection
            };
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
    }
}
