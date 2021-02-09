namespace MailSender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_smtp_data : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "HostSmtp", c => c.String());
            AddColumn("dbo.AspNetUsers", "EnableSsl", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "Port", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "SenderEmail", c => c.String());
            AddColumn("dbo.AspNetUsers", "SenderEmailPassword", c => c.String());
            AddColumn("dbo.AspNetUsers", "SenderName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SenderName");
            DropColumn("dbo.AspNetUsers", "SenderEmailPassword");
            DropColumn("dbo.AspNetUsers", "SenderEmail");
            DropColumn("dbo.AspNetUsers", "Port");
            DropColumn("dbo.AspNetUsers", "EnableSsl");
            DropColumn("dbo.AspNetUsers", "HostSmtp");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
