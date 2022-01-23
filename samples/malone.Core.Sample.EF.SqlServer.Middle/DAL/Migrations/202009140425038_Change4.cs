namespace malone.Core.Sample.EF.SqlServer.Middle.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Change4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskItems", "Done", c => c.Boolean(nullable: false));
            DropColumn("dbo.TaskItems", "Pending");
        }

        public override void Down()
        {
            AddColumn("dbo.TaskItems", "Pending", c => c.Boolean(nullable: false));
            DropColumn("dbo.TaskItems", "Done");
        }
    }
}
