namespace malone.Core.Sample.EF.SqlServer.Middle.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TodoLists", "UserId", "dbo.Users");
            DropIndex("dbo.TodoLists", "IX_User_Id");
            AlterColumn("dbo.TodoLists", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.TodoLists", "UserId");
            AddForeignKey("dbo.TodoLists", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TodoLists", "UserId", "dbo.Users");
            DropIndex("dbo.TodoLists", new[] { "UserId" });
            AlterColumn("dbo.TodoLists", "UserId", c => c.Int());
            CreateIndex("dbo.TodoLists", "UserId", name: "IX_User_Id");
            AddForeignKey("dbo.TodoLists", "UserId", "dbo.Users", "Id");
        }
    }
}
