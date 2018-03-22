namespace Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vasiliystan_9dc0430cf2cd43eba6af748007b63d9e : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Guid(),
                        IsActivate = c.Boolean(),
                        AccountName = c.String(),
                        Description = c.String(),
                        Email = c.String(),
                        Note = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EnumTypes",
                c => new
                    {
                        RowId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Id = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.RowId)
                .Index(t => t.Id, unique: true);
            
            CreateTable(
                "dbo.EnumValues",
                c => new
                    {
                        RowId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Int(nullable: false),
                        Id = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Type_RowId = c.Int(),
                    })
                .PrimaryKey(t => t.RowId)
                .ForeignKey("dbo.EnumTypes", t => t.Type_RowId)
                .Index(t => t.Id, unique: true)
                .Index(t => t.Type_RowId);
            
            CreateTable(
                "dbo.Seeding",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsSeed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.webpages_Membership",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        CreateDate = c.DateTime(),
                        ConfirmationToken = c.String(maxLength: 128),
                        IsConfirmed = c.Boolean(),
                        LastPasswordFailureDate = c.DateTime(),
                        PasswordFailuresSinceLastSuccess = c.Int(nullable: false),
                        Password = c.String(nullable: false, maxLength: 128),
                        PasswordChangedDate = c.DateTime(),
                        PasswordSalt = c.String(nullable: false, maxLength: 128),
                        PasswordVerificationToken = c.String(maxLength: 128),
                        PasswordVerificationTokenExpirationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.webpages_OAuthMembership",
                c => new
                    {
                        Provider = c.String(nullable: false, maxLength: 30),
                        ProviderUserId = c.String(nullable: false, maxLength: 100),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Provider, t.ProviderUserId });
            
            CreateTable(
                "dbo.webpages_Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RoleId)
                .Index(t => t.RoleName, unique: true);
            
            CreateTable(
                "dbo.webpages_UsersInRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.webpages_Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.webpages_RolesInRoles",
                c => new
                    {
                        Child = c.Int(nullable: false),
                        Parent = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Child, t.Parent })
                .ForeignKey("dbo.webpages_Roles", t => t.Child)
                .ForeignKey("dbo.webpages_Roles", t => t.Parent)
                .Index(t => t.Child)
                .Index(t => t.Parent);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.webpages_UsersInRoles", "RoleId", "dbo.webpages_Roles");
            DropForeignKey("dbo.webpages_RolesInRoles", "Parent", "dbo.webpages_Roles");
            DropForeignKey("dbo.webpages_RolesInRoles", "Child", "dbo.webpages_Roles");
            DropForeignKey("dbo.EnumValues", "Type_RowId", "dbo.EnumTypes");
            DropIndex("dbo.webpages_RolesInRoles", new[] { "Parent" });
            DropIndex("dbo.webpages_RolesInRoles", new[] { "Child" });
            DropIndex("dbo.webpages_UsersInRoles", new[] { "RoleId" });
            DropIndex("dbo.webpages_Roles", new[] { "RoleName" });
            DropIndex("dbo.EnumValues", new[] { "Type_RowId" });
            DropIndex("dbo.EnumValues", new[] { "Id" });
            DropIndex("dbo.EnumTypes", new[] { "Id" });
            DropTable("dbo.webpages_RolesInRoles");
            DropTable("dbo.webpages_UsersInRoles");
            DropTable("dbo.webpages_Roles");
            DropTable("dbo.webpages_OAuthMembership");
            DropTable("dbo.webpages_Membership");
            DropTable("dbo.Seeding");
            DropTable("dbo.EnumValues");
            DropTable("dbo.EnumTypes");
            DropTable("dbo.Accounts");
        }
    }
}
