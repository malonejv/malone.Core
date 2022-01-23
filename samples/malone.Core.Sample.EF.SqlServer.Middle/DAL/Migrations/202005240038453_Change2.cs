namespace malone.Core.Sample.EF.SqlServer.Middle.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TODOLISTS", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TODOLISTS", "Date");
        }
    }
}
