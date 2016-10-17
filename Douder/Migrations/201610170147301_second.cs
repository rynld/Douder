namespace Douder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Messages", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Messages", "Info", c => c.String(nullable: false));
            CreateIndex("dbo.Messages", "UserId");
            AddForeignKey("dbo.Messages", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "UserId" });
            AlterColumn("dbo.Messages", "Info", c => c.String());
            AlterColumn("dbo.Messages", "Name", c => c.String());
            DropColumn("dbo.Messages", "UserId");
        }
    }
}
