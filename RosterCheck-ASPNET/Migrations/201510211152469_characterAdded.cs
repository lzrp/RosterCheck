namespace RosterCheck_ASPNET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class characterAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "Audit_NumberOfIssues", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "Audit_EmptySockets", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "Audit_EmptySockets");
            DropColumn("dbo.Characters", "Audit_NumberOfIssues");
        }
    }
}
