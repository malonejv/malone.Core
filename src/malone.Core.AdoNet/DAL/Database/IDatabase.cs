using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace malone.Core.AdoNet.DAL.Database
{
    public interface IDatabase
    {
        IDbConnection CreateConnection();
        void CloseConnection(IDbConnection connection);
        IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection);
        IDataAdapter CreateAdapter(IDbCommand command);
        IDbDataParameter CreateParameter(IDbCommand command);
        void AddParameter(IDbCommand command, IDbDataParameter parameter, object type);
    }
}