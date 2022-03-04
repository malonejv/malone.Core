namespace malone.Core.Sample.EF.SqlServer.Middle.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Change6 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TaskItems", name: "TodoList_Id", newName: "TodoListId");
            RenameColumn(table: "dbo.TodoLists", name: "User_Id", newName: "UserId");
        }

        public override void Down()
        {
            RenameColumn(table: "dbo.TodoLists", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.TaskItems", name: "TodoListId", newName: "TodoList_Id");
        }
    }
}
