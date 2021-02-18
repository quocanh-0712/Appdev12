namespace WebApplication11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hjkw : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraineeCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TraineeId = c.String(maxLength: 128),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TraineeId)
                .Index(t => t.TraineeId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Email = c.String(),
                        FullName = c.String(),
                        Phone = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainees", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TraineeCourses", "TraineeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TraineeCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.Trainees", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TraineeCourses", new[] { "CourseId" });
            DropIndex("dbo.TraineeCourses", new[] { "TraineeId" });
            DropTable("dbo.Trainees");
            DropTable("dbo.TraineeCourses");
        }
    }
}
