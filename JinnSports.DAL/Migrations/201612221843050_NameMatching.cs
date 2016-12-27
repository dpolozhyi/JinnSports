namespace JinnSports.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameMatching : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SportEvents", new[] { "SportType_Id" });
            DropIndex("dbo.Results", new[] { "SportEvent_Id" });
            DropIndex("dbo.Results", new[] { "Team_Id" });
            DropIndex("dbo.Teams", new[] { "SportType_Id" });
            CreateTable(
                "dbo.Conformities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsConfirmed = c.Boolean(nullable: false),
                        ExistedTeam_Id = c.Int(),
                        InputTeam_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.ExistedTeam_Id)
                .ForeignKey("dbo.Teams", t => t.InputTeam_Id)
                .Index(t => t.ExistedTeam_Id)
                .Index(t => t.InputTeam_Id);
            
            CreateTable(
                "dbo.TeamNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .Index(t => t.Team_Id);
            
            AddColumn("dbo.Results", "IsHome", c => c.Boolean(nullable: false));
            AlterColumn("dbo.SportEvents", "SportType_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Results", "Score", c => c.Int());
            AlterColumn("dbo.Results", "SportEvent_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Results", "Team_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Teams", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Teams", "SportType_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.SportTypes", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Teams", "SportType_Id");
            CreateIndex("dbo.Results", "SportEvent_Id");
            CreateIndex("dbo.Results", "Team_Id");
            CreateIndex("dbo.SportEvents", "SportType_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conformities", "InputTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.Conformities", "ExistedTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.TeamNames", "Team_Id", "dbo.Teams");
            DropIndex("dbo.SportEvents", new[] { "SportType_Id" });
            DropIndex("dbo.Results", new[] { "Team_Id" });
            DropIndex("dbo.Results", new[] { "SportEvent_Id" });
            DropIndex("dbo.TeamNames", new[] { "Team_Id" });
            DropIndex("dbo.Teams", new[] { "SportType_Id" });
            DropIndex("dbo.Conformities", new[] { "InputTeam_Id" });
            DropIndex("dbo.Conformities", new[] { "ExistedTeam_Id" });
            AlterColumn("dbo.SportTypes", "Name", c => c.String());
            AlterColumn("dbo.Teams", "SportType_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Name", c => c.String());
            AlterColumn("dbo.Results", "Team_Id", c => c.Int());
            AlterColumn("dbo.Results", "SportEvent_Id", c => c.Int());
            AlterColumn("dbo.Results", "Score", c => c.Int(nullable: false));
            AlterColumn("dbo.SportEvents", "SportType_Id", c => c.Int());
            DropColumn("dbo.Results", "IsHome");
            DropTable("dbo.TeamNames");
            DropTable("dbo.Conformities");
            CreateIndex("dbo.Teams", "SportType_Id");
            CreateIndex("dbo.Results", "Team_Id");
            CreateIndex("dbo.Results", "SportEvent_Id");
            CreateIndex("dbo.SportEvents", "SportType_Id");
        }
    }
}
