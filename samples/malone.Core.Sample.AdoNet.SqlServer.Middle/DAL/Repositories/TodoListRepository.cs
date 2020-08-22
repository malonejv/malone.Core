using System.Data;
using malone.Core.AdoNet.Repositories;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Context;
using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.DAL.Repositories
{
    public class TodoListRepository : AdoNetRepository<TodoList>
    {
        public TodoListRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }

        #region Overridden Methods

        #region Crud Operations

        #region Get

        protected override void ConfigureCommandForGetAll(IDbCommand command, bool includeDeleted, string includeProperties)
        {
            string query = @"SELECT Id, Name, Date, IsDeleted
                               FROM TodoLists;";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        protected override void ConfigureCommandForGet(IDbCommand command, bool includeDeleted, string includeProperties)
        {
            string query = @"SELECT Id, Name, Date, IsDeleted
                               FROM TodoLists
                              WHERE LOWER(Name) like LOWER('%' + @Name + '%');";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        protected override void ConfigureCommandForGetById(IDbCommand command, bool includeDeleted, string includeProperties)
        {
            string query = @"SELECT Id, Name, Date, IsDeleted
                               FROM TodoLists
                              WHERE Id = @Id;";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        protected override void ConfigureCommandForGetEntity(IDbCommand command, bool includeDeleted, string includeProperties)
        {
            string query = @"SELECT Id, Name, Date, IsDeleted
                               FROM TodoLists
                              WHERE Id = @Id;";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        #endregion

        #region Add

        protected override void ConfigureCommandForInsert(IDbCommand command)
        {
            string query = @"INSERT INTO TodoLists (Name, Date, IsDeleted) VALUES ( @Name, @Date, @IsDeleted );";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        #endregion

        #region Update

        protected override void ConfigureCommandForUpdate(IDbCommand command)
        {
            string query = @"UPDATE TodoLists SET 
                                Name = @Name,
                                Date = @Date,
                                IsDeleted = @IsDeleted
                              WHERE Id = @Id;";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        #endregion

        #region Delete

        protected override void ConfigureCommandForDelete(IDbCommand command)
        {
            string query = "";
            if (true)
            {
                query = @"UPDATE TodoLists SET 
                                IsDeleted = @IsDeleted
                           WHERE Id = @Id;";
            }
            else
            {
                query = $"DELETE FROM TodoLists WHERE Id = @Id;";
            }

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        #endregion

        #endregion

        protected override TodoList Map(DataRow row)
        {
            TodoList todoList = null;
            if (!row.IsNull())
            {
                todoList = new TodoList();
                todoList.Id = row.AsIntOrDefault("Id");
                todoList.Name = row.AsString("Name");
                todoList.Date = row.AsDate("Date");
                todoList.IsDeleted = row.AsBooleanOrDefault("IsDeleted");
            }
            return todoList;
        }

        #endregion

    }
}
