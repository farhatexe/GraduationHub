namespace GraduationHub.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FrequentlyAskedQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Question = c.String(nullable: false, maxLength: 150),
                        Answer = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GraduateInformation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 75),
                        Address_Street = c.String(maxLength: 75),
                        Address_City = c.String(maxLength: 75),
                        Address_State = c.String(maxLength: 2),
                        Address_Zipcode = c.String(maxLength: 10),
                        StudentEmail = c.String(maxLength: 75),
                        ParentEmail = c.String(maxLength: 75),
                        EnrolledFineArts = c.Boolean(nullable: false),
                        EnrolledAcademicClasses = c.Boolean(nullable: false),
                        WillParticiateInGraduation = c.Boolean(nullable: false),
                        TakenKeysWorldView = c.Boolean(nullable: false),
                        TakenApprovedWorldView = c.Boolean(),
                        WillSecureAnnouncements = c.Boolean(nullable: false),
                        NeedCapAndGown = c.Boolean(nullable: false),
                        Height = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GraduatingClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 75),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImportantDates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DueDate = c.DateTime(nullable: false),
                        Comments = c.String(maxLength: 500),
                        GraduatingClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GraduatingClasses", t => t.GraduatingClassId, cascadeDelete: true)
                .Index(t => t.GraduatingClassId);
            
            CreateTable(
                "dbo.Invitations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InviteeName = c.String(maxLength: 75),
                        GraduatingClassId = c.Int(nullable: false),
                        Email = c.String(maxLength: 75),
                        InviteCode = c.Guid(),
                        HasBeenRedeemed = c.Boolean(nullable: false),
                        HasBeenSent = c.Boolean(nullable: false),
                        IsTeacher = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GraduatingClasses", t => t.GraduatingClassId, cascadeDelete: true)
                .Index(t => t.GraduatingClassId);
            
            CreateTable(
                "dbo.LogActions",
                c => new
                    {
                        LogActionId = c.Int(nullable: false, identity: true),
                        PerformedAt = c.DateTime(nullable: false),
                        Controller = c.String(maxLength: 125),
                        Action = c.String(maxLength: 125),
                        Description = c.String(maxLength: 125),
                        PerformedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LogActionId)
                .ForeignKey("dbo.AspNetUsers", t => t.PerformedBy_Id)
                .Index(t => t.PerformedBy_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
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
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.StudentExpressions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(maxLength: 128),
                        Type = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                        DateSubmitted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.StudentPictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(maxLength: 128),
                        ImageName = c.String(nullable: false, maxLength: 75),
                        Description = c.String(maxLength: 175),
                        ImageType = c.Int(nullable: false),
                        ImageData = c.Binary(),
                        DateSubmitted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentPictures", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentExpressions", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.LogActions", "PerformedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Invitations", "GraduatingClassId", "dbo.GraduatingClasses");
            DropForeignKey("dbo.ImportantDates", "GraduatingClassId", "dbo.GraduatingClasses");
            DropIndex("dbo.StudentPictures", new[] { "StudentId" });
            DropIndex("dbo.StudentExpressions", new[] { "StudentId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.LogActions", new[] { "PerformedBy_Id" });
            DropIndex("dbo.Invitations", new[] { "GraduatingClassId" });
            DropIndex("dbo.ImportantDates", new[] { "GraduatingClassId" });
            DropTable("dbo.StudentPictures");
            DropTable("dbo.StudentExpressions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.LogActions");
            DropTable("dbo.Invitations");
            DropTable("dbo.ImportantDates");
            DropTable("dbo.GraduatingClasses");
            DropTable("dbo.GraduateInformation");
            DropTable("dbo.FrequentlyAskedQuestions");
        }
    }
}
