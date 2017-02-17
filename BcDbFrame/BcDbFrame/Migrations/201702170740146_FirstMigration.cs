namespace BcDbFrame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaseApiRecord",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApiCode = c.String(maxLength: 200),
                        Visitor = c.String(),
                        VisitDetail = c.String(nullable: false),
                        Input = c.String(),
                        Output = c.String(),
                        TimeLong = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BaseApp",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        User_ID = c.Guid(nullable: false),
                        Secret = c.String(nullable: false, maxLength: 200),
                        Name = c.String(nullable: false, maxLength: 200),
                        Description = c.String(),
                        Category = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseUser", t => t.User_ID, cascadeDelete: true)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.BaseRole",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        ParentID = c.Guid(),
                        Category = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseRole", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.BaseFunction",
                c => new
                    {
                        Controller = c.String(nullable: false, maxLength: 200),
                        Action = c.String(nullable: false, maxLength: 200),
                        Category = c.Int(nullable: false),
                        ApiCode = c.String(nullable: false, maxLength: 200),
                        Id = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Controller, t.Action });
            
            CreateTable(
                "dbo.BaseUser",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Account = c.String(nullable: false, maxLength: 200),
                        Password = c.String(nullable: false, maxLength: 200),
                        Category = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BaseUserDetail",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 200),
                        Phone = c.String(maxLength: 200),
                        Sex = c.Int(nullable: false),
                        Birthday = c.DateTime(),
                        Height = c.Decimal(precision: 18, scale: 2),
                        Note = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseUser", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.BaseLog",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Message = c.String(nullable: false),
                        Category = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BaseRoleApp",
                c => new
                    {
                        Role_Id = c.Guid(nullable: false),
                        App_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.App_Id })
                .ForeignKey("dbo.BaseRole", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.BaseApp", t => t.App_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.App_Id);
            
            CreateTable(
                "dbo.BaseRoleFunction",
                c => new
                    {
                        Role_Id = c.Guid(nullable: false),
                        Controller = c.String(nullable: false, maxLength: 200),
                        Action = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.Controller, t.Action })
                .ForeignKey("dbo.BaseRole", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.BaseFunction", t => new { t.Controller, t.Action }, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => new { t.Controller, t.Action });
            
            CreateTable(
                "dbo.BaseRoleUser",
                c => new
                    {
                        Role_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id })
                .ForeignKey("dbo.BaseRole", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.BaseUser", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BaseRoleUser", "User_Id", "dbo.BaseUser");
            DropForeignKey("dbo.BaseRoleUser", "Role_Id", "dbo.BaseRole");
            DropForeignKey("dbo.BaseUserDetail", "Id", "dbo.BaseUser");
            DropForeignKey("dbo.BaseApp", "User_ID", "dbo.BaseUser");
            DropForeignKey("dbo.BaseRole", "ParentID", "dbo.BaseRole");
            DropForeignKey("dbo.BaseRoleFunction", new[] { "Controller", "Action" }, "dbo.BaseFunction");
            DropForeignKey("dbo.BaseRoleFunction", "Role_Id", "dbo.BaseRole");
            DropForeignKey("dbo.BaseRoleApp", "App_Id", "dbo.BaseApp");
            DropForeignKey("dbo.BaseRoleApp", "Role_Id", "dbo.BaseRole");
            DropIndex("dbo.BaseRoleUser", new[] { "User_Id" });
            DropIndex("dbo.BaseRoleUser", new[] { "Role_Id" });
            DropIndex("dbo.BaseRoleFunction", new[] { "Controller", "Action" });
            DropIndex("dbo.BaseRoleFunction", new[] { "Role_Id" });
            DropIndex("dbo.BaseRoleApp", new[] { "App_Id" });
            DropIndex("dbo.BaseRoleApp", new[] { "Role_Id" });
            DropIndex("dbo.BaseUserDetail", new[] { "Id" });
            DropIndex("dbo.BaseRole", new[] { "ParentID" });
            DropIndex("dbo.BaseApp", new[] { "User_ID" });
            DropTable("dbo.BaseRoleUser");
            DropTable("dbo.BaseRoleFunction");
            DropTable("dbo.BaseRoleApp");
            DropTable("dbo.BaseLog");
            DropTable("dbo.BaseUserDetail");
            DropTable("dbo.BaseUser");
            DropTable("dbo.BaseFunction");
            DropTable("dbo.BaseRole");
            DropTable("dbo.BaseApp");
            DropTable("dbo.BaseApiRecord");
        }
    }
}
