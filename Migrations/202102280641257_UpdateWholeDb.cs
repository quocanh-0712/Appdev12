namespace WebApplication11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWholeDb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trainees", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Trainees", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Trainers", new[] { "TopicId" });
            AddColumn("dbo.AspNetUsers", "TopicId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "TopicId");
            DropTable("dbo.Trainees");
            DropTable("dbo.Trainers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        WorkingPlace = c.String(),
                        Phone = c.String(),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.AspNetUsers", new[] { "TopicId" });
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetUsers", "TopicId");
            CreateIndex("dbo.Trainers", "TopicId");
            CreateIndex("dbo.Trainees", "ApplicationUser_Id");
            AddForeignKey("dbo.Trainees", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
