using malone.Core.AdoNet.Database;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace malone.Core.AdoNet.Oracle
{
    public class OracleDatabase : IDatabase
    {
        private const string ID = "Id";
        private const string IS_DELETED = "IsDeleted";

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

        public void AddParameter(IDbCommand command, IDbDataParameter parameter, object type)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            OracleParameter oracleParameter = (OracleParameter)parameter;
            oracleParameter.OracleDbType = (OracleDbType)type;

            oracleCommand.Parameters.Add(oracleParameter);
        }

        public void AddParameterId(IDbCommand command, object id)
        {
            var parameterId = (OracleParameter)CreateParameter(command);
            parameterId.ParameterName = ID;
            parameterId.Value = (OracleDbType)id;
            parameterId.OracleDbType = OracleDbType.Decimal;

            command.Parameters.Add(parameterId);
        }

        public void AddParameterIsDeleted(IDbCommand command, object isDeleted)
        {
            var parameterIsDeleted = (OracleParameter)CreateParameter(command);
            parameterIsDeleted.ParameterName = IS_DELETED;
            parameterIsDeleted.Value = Convert.ToInt32((bool)isDeleted);
            parameterIsDeleted.OracleDbType = OracleDbType.Int32;

            command.Parameters.Add(parameterIsDeleted);
        }

    }
}
