using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace malone.Core.DAL.AdoNet.Provider
{
    public interface IDatabase
    {
        IDbConnection CreateConnection();
        void CloseConnection(IDbConnection connection);
        IDbCommand CreateCommand(IDbConnection connection, CommandType commandType, string commandText = "");
        IDataAdapter CreateAdapter(IDbCommand command);
        IDbDataParameter CreateParameter(IDbCommand command);
        void AddParameter(IDbCommand command, IDbDataParameter parameter, object type);
        void AddParameterId(IDbCommand command,object id);
        void AddParameterIsDeleted(IDbCommand command,object isDeleted);

    }
}
