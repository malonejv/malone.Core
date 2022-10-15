namespace malone.Core.Sample.EF.Firebird.Middle.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.UsersRoles",
                c => new
                {
                    UserId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.TODOLISTS",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Date = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.TASKITEMS",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 100),
                    IsDeleted = c.Boolean(nullable: false),
                    TodoList_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TODOLISTS", t => t.TodoList_Id, cascadeDelete: true, name: "FK_TASK_TODO_TodoList_Id")
                .Index(t => t.TodoList_Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.UsersClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.UsersLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.UsersRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.TASKITEMS", "TodoList_Id", "dbo.TODOLISTS");
            DropForeignKey("dbo.UsersRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.UsersLogins", new[] { "UserId" });
            DropIndex("dbo.UsersClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.TASKITEMS", new[] { "TodoList_Id" });
            DropIndex("dbo.UsersRoles", new[] { "RoleId" });
            DropIndex("dbo.UsersRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropTable("dbo.UsersLogins");
            DropTable("dbo.UsersClaims");
            DropTable("dbo.Users");
            DropTable("dbo.TASKITEMS");
            DropTable("dbo.TODOLISTS");
            DropTable("dbo.UsersRoles");
            DropTable("dbo.Roles");
        }
    }
}
