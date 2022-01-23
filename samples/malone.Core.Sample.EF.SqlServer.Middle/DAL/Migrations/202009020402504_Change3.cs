namespace malone.Core.Sample.EF.SqlServer.Middle.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Change3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskItems", "Pending", c => c.Boolean(nullable: false));
            AlterColumn("dbo.TodoLists", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.TaskItems", "Description", c => c.String(nullable: false, maxLength: 100));
        }

        public override void Down()
        {
            AlterColumn("dbo.TaskItems", "Description", c => c.String(maxLength: 100));
            AlterColumn("dbo.TodoLists", "Name", c => c.String(maxLength: 100));
            DropColumn("dbo.TaskItems", "Pending");
        }
    }
}
