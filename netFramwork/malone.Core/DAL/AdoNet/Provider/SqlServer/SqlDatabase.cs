using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace malone.Core.DAL.AdoNet.Provider.SqlServer
{
    public class SqlDatabase : IDatabase
    {
        private const string ID = "@Id";
        private const string IS_DELETED = "@IsDeleted";

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

        public IDbCommand CreateCommand(IDbConnection connection, CommandType commandType, string commandText = "")
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
        public void AddParameter(IDbCommand command, IDbDataParameter parameter, object type)
        {
            SqlParameter sqlParameter = (SqlParameter)parameter;
            sqlParameter.SqlDbType = (SqlDbType)type;

            command.Parameters.Add(sqlParameter);
        }

        public void AddParameterId(IDbCommand command, object id)
        {
            var parameterId = (SqlParameter)CreateParameter(command);
            parameterId.ParameterName = ID;
            parameterId.Value = (SqlDbType)id;
            parameterId.SqlDbType = SqlDbType.Int;

            command.Parameters.Add(parameterId);
        }

        public void AddParameterIsDeleted(IDbCommand command, object isDeleted)
        {
            var parameterIsDeleted = (SqlParameter)CreateParameter(command);
            parameterIsDeleted.ParameterName = IS_DELETED;
            parameterIsDeleted.Value = isDeleted;
            parameterIsDeleted.SqlDbType = SqlDbType.Bit;

            command.Parameters.Add(parameterIsDeleted);
        }


    }
}
