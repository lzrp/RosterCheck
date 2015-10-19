namespace RosterCheck_ASPNET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GuildModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastModified = c.Long(nullable: false),
                        Name = c.String(),
                        Realm = c.String(),
                        Level = c.Int(nullable: false),
                        Side = c.Int(nullable: false),
                        AchievementPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rank = c.Int(nullable: false),
                        RankName = c.String(),
                        Character_Id = c.Int(),
                        GuildModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.Character_Id)
                .ForeignKey("dbo.GuildModels", t => t.GuildModel_Id)
                .Index(t => t.Character_Id)
                .Index(t => t.GuildModel_Id);
            
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Realm = c.String(),
                        Class = c.Int(nullable: false),
                        ClassName = c.String(),
                        Race = c.Int(nullable: false),
                        RaceName = c.String(),
                        Gender = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        LastModified = c.Int(nullable: false),
                        Spec_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specs", t => t.Spec_Id)
                .Index(t => t.Spec_Id);
            
            CreateTable(
                "dbo.Specs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Role = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "GuildModel_Id", "dbo.GuildModels");
            DropForeignKey("dbo.Members", "Character_Id", "dbo.Characters");
            DropForeignKey("dbo.Characters", "Spec_Id", "dbo.Specs");
            DropIndex("dbo.Characters", new[] { "Spec_Id" });
            DropIndex("dbo.Members", new[] { "GuildModel_Id" });
            DropIndex("dbo.Members", new[] { "Character_Id" });
            DropTable("dbo.Specs");
            DropTable("dbo.Characters");
            DropTable("dbo.Members");
            DropTable("dbo.GuildModels");
        }
    }
}
