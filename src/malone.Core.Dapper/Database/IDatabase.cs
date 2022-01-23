using System.Data;

namespace malone.Core.Dapper.Database
{
    public interface IDatabase
    {
        IDbConnection CreateConnection();
        void CloseConnection(IDbConnection connection);
        IDataAdapter CreateAdapter(IDbCommand command);
    }
}