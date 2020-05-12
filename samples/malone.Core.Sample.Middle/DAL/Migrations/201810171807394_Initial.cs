namespace malone.Core.Sample.Middle.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TodoLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaskItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 100),
                        IsDeleted = c.Boolean(nullable: false),
                        TodoList_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TodoLists", t => t.TodoList_Id, cascadeDelete: true)
                .Index(t => t.TodoList_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskItems", "TodoList_Id", "dbo.TodoLists");
            DropIndex("dbo.TaskItems", new[] { "TodoList_Id" });
            DropTable("dbo.TaskItems");
            DropTable("dbo.TodoLists");
        }
    }
}
