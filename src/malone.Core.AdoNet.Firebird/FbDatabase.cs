using FirebirdSql.Data.FirebirdClient;
using malone.Core.AdoNet.Database;
using System;
using System.Data;

namespace malone.Core.AdoNet.Firebird
{
    public class FbDatabase : IDatabase
    {
        private string ConnectionString { get; set; }


        public FbDatabase(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new FbConnection(ConnectionString);
        }


        public void CloseConnection(IDbConnection connection)
        {
            var fbconnection = (FbConnection)connection;
            fbconnection.Close();
            fbconnection.Dispose();
        }

        public IDataAdapter CreateAdapter(IDbCommand command)
        {
            return new FbDataAdapter((FbCommand)command);
        }

        public IDbDataParameter CreateParameter(IDbCommand command)
        {
            FbCommand fbCommand = (FbCommand)command;
            return fbCommand.CreateParameter();
        }

        public void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType)
        {
            FbCommand fbCommand = FbDatabase.ValidateCommand(command);
            if (parameterType is FbDbType)
            {
                var dbType = (FbDbType)parameterType;
                FbParameter fbParameter = fbCommand.Parameters.Add(parameterName, dbType);
                fbParameter.Value = value;
                fbParameter.Direction = parameterdirection;
            }
            else
            {
                //TODO: manejar con errores del core.
                throw new InvalidOperationException(string.Format("FbType unrecognized: {0}", (object)parameterName));
            }
        }

        public void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType, int size)
        {
            FbCommand fbCommand = FbDatabase.ValidateCommand(command);
            if (parameterType is FbDbType)
            {
                var dbType = (FbDbType)parameterType;
                FbParameter fbParameter = fbCommand.Parameters.Add(parameterName, dbType, size);
                fbParameter.Value = value;
                fbParameter.Direction = parameterdirection;
            }
            else
            {
                //TODO: manejar con errores del core.
                throw new InvalidOperationException(string.Format("FbType unrecognized: {0}", (object)parameterName));
            }
        }

        private static FbCommand ValidateCommand(IDbCommand command)
        {
            if (!(command is FbCommand fbCommand))
                throw new InvalidOperationException("Error");
            //TODO: manejar con errores del core.
            //string.Format((IFormatProvider)CultureInfo.CurrentCulture, Resources.FbCommandExpected, (object)command.GetType().FullName))
            return fbCommand;
        }
    }
}
