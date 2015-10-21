namespace RosterCheck_ASPNET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastModifiedFromIntToLong : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Characters", "LastModified", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Characters", "LastModified", c => c.Int(nullable: false));
        }
    }
}
