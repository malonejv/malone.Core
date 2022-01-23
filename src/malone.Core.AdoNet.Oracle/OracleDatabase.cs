using malone.Core.AdoNet.Database;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace malone.Core.AdoNet.Oracle
{
    public class OracleDatabase : IDatabase
    {
        private string ConnectionString { get; set; }


        public OracleDatabase(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new OracleConnection(ConnectionString);
        }


        public void CloseConnection(IDbConnection connection)
        {
            var oracleconnection = (OracleConnection)connection;
            oracleconnection.Close();
            oracleconnection.Dispose();
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            return new OracleCommand()
            {
                CommandText = commandText,
                CommandType = commandType,
                Connection = (OracleConnection)connection
            };
        }

        public IDataAdapter CreateAdapter(IDbCommand command)
        {
            return new OracleDataAdapter((OracleCommand)command);
        }

        public IDbDataParameter CreateParameter(IDbCommand command)
        {
            OracleCommand oracleCommand = (OracleCommand)command;

            return oracleCommand.CreateParameter();
        }

        public void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType)
        {
            throw new NotImplementedException();
        }

        public void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType, int size)
        {
            throw new NotImplementedException();
        }
    }
}
