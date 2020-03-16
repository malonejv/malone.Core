using malone.Core.DAL.AdoNet.Factory;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace malone.Core.DAL.AdoNet.OracleClient
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
    }
}
