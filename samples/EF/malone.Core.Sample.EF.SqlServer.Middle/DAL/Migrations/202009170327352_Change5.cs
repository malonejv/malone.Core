namespace malone.Core.Sample.EF.SqlServer.Middle.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Change5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TodoLists", "User_Id", c => c.Int());
            CreateIndex("dbo.TodoLists", "User_Id");
            AddForeignKey("dbo.TodoLists", "User_Id", "dbo.Users", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.TodoLists", "User_Id", "dbo.Users");
            DropIndex("dbo.TodoLists", new[] { "User_Id" });
            DropColumn("dbo.TodoLists", "User_Id");
        }
    }
}
