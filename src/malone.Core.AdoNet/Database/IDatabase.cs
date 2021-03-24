using System.Data;

namespace malone.Core.AdoNet.Database
{
    public interface IDatabase
    {
        IDbConnection CreateConnection();
        void CloseConnection(IDbConnection connection);
        IDataAdapter CreateAdapter(IDbCommand command);
        IDbDataParameter CreateParameter(IDbCommand command);
        void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType);
        void AddCommandParameter(IDbCommand command, string parameterName, object value, ParameterDirection parameterdirection, object parameterType, int size);
    }
}