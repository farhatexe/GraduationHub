namespace GraduationHub.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExpressionAndPicturesToApplicationUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentExpressions", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentPictures", "StudentId", "dbo.AspNetUsers");
            DropIndex("dbo.StudentExpressions", new[] { "StudentId" });
            DropIndex("dbo.StudentPictures", new[] { "StudentId" });
            AddColumn("dbo.AspNetUsers", "IsStudent", c => c.Boolean(nullable: false));
            AlterColumn("dbo.StudentExpressions", "StudentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.StudentPictures", "StudentId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.StudentExpressions", "StudentId");
            CreateIndex("dbo.StudentPictures", "StudentId");
            AddForeignKey("dbo.StudentExpressions", "StudentId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StudentPictures", "StudentId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentPictures", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentExpressions", "StudentId", "dbo.AspNetUsers");
            DropIndex("dbo.StudentPictures", new[] { "StudentId" });
            DropIndex("dbo.StudentExpressions", new[] { "StudentId" });
            AlterColumn("dbo.StudentPictures", "StudentId", c => c.String(maxLength: 128));
            AlterColumn("dbo.StudentExpressions", "StudentId", c => c.String(maxLength: 128));
            DropColumn("dbo.AspNetUsers", "IsStudent");
            CreateIndex("dbo.StudentPictures", "StudentId");
            CreateIndex("dbo.StudentExpressions", "StudentId");
            AddForeignKey("dbo.StudentPictures", "StudentId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.StudentExpressions", "StudentId", "dbo.AspNetUsers", "Id");
        }
    }
}
