using malone.Core.AdoNet.Repositories;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Context;
using malone.Core.Sample.AdoNet.Firebird.Middle.EL.Model;
using System.Data;
namespace malone.Core.Sample.AdoNet.Firebird.Middle.DAL.Repositories
{
    public class TaskItemRepository : Repository<TaskItem>
    {
        public TaskItemRepository(IContext context, ILogger logger) : base(context, logger)
        {
        }

        #region Overridden Methods

        #region Crud Operations

        #region Get

        protected override void ConfigureCommandForGetAll(IDbCommand command, bool includeDeleted, string includeProperties)
        {
            string query = @"SELECT Id, Description, Done, IsDeleted
                               FROM TaskItems;";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        protected override void ConfigureCommandForGet(IDbCommand command, bool includeDeleted, string includeProperties)
        {
            string query = @"SELECT Id, Description, Done, IsDeleted
                               FROM TaskItems
                              WHERE LOWER(Description) like LOWER('%' + @Description + '%');";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        protected override void ConfigureCommandForGetById(IDbCommand command, bool includeDeleted, string includeProperties)
        {
            string query = @"SELECT Id, Description, Done, IsDeleted
                               FROM TaskItems
                              WHERE Id = @Id;";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        protected override void ConfigureCommandForGetEntity(IDbCommand command, bool includeDeleted, string includeProperties)
        {
            string query = @"SELECT Id, Description, Done, IsDeleted
                               FROM TaskItems
                              WHERE Id = @Id;";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        #endregion

        #region Add

        protected override void ConfigureCommandForInsert(IDbCommand command)
        {
            string query = @"INSERT INTO TaskItems (Description, Done, IsDeleted) VALUES ( @Description, @Done, @IsDeleted );";

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        #endregion

        #region Update

        protected override void ConfigureCommandForUpdate(IDbCommand command)
        {
            string query = @"UPDATE TaskItems SET 
                                Description = @Description,
                                Done = @Done,
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
                query = @"UPDATE TaskItems SET 
                                IsDeleted = @IsDeleted
                           WHERE Id = @Id;";
            }
            else
            {
                query = $"DELETE FROM TaskItems WHERE Id = @Id;";
            }

            command.CommandText = query;
            command.CommandType = CommandType.Text;
        }

        #endregion

        #endregion

        protected override TaskItem Map(DataRow row)
        {
            TaskItem todoList = null;
            if (!row.IsNull())
            {
                todoList = new TaskItem();
                todoList.Id = row.AsIntOrDefault("Id");
                todoList.Description = row.AsString("Description");
                todoList.Done = row.AsBooleanOrDefault("Done");
                todoList.IsDeleted = row.AsBooleanOrDefault("IsDeleted");
            }
            return todoList;
        }

        #endregion

    }
}
