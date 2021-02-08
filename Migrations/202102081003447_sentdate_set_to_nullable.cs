namespace MailSender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sentdate_set_to_nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Emails", "SentDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Emails", "SentDate", c => c.DateTime(nullable: false));
        }
    }
}
