namespace malone.Core.Sample.EF.Firebird.Middle.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class change1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TODOLISTS", "User_Id", c => c.Int());
            AddColumn("dbo.TASKITEMS", "Done", c => c.Boolean(nullable: false));
            CreateIndex("dbo.TODOLISTS", "User_Id");
            AddForeignKey("dbo.TODOLISTS", "User_Id", "dbo.Users", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.TODOLISTS", "User_Id", "dbo.Users");
            DropIndex("dbo.TODOLISTS", new[] { "User_Id" });
            DropColumn("dbo.TASKITEMS", "Done");
            DropColumn("dbo.TODOLISTS", "User_Id");
        }
    }
}
