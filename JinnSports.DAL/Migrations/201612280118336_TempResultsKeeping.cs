namespace JinnSports.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TempResultsKeeping : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Conformities", "ExistedTeam_Id", "dbo.Teams");
            DropIndex("dbo.Conformities", new[] { "ExistedTeam_Id" });
            CreateTable(
                "dbo.TempResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(),
                        IsHome = c.Boolean(nullable: false),
                        Team_Id = c.Int(),
                        TempSportEvent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .ForeignKey("dbo.TempSportEvents", t => t.TempSportEvent_Id)
                .Index(t => t.Team_Id)
                .Index(t => t.TempSportEvent_Id);
            
            CreateTable(
                "dbo.TempSportEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        SportType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SportTypes", t => t.SportType_Id)
                .Index(t => t.SportType_Id);
            
            AddColumn("dbo.Conformities", "InputName", c => c.String());
            AddColumn("dbo.Conformities", "ExistedName", c => c.String());
            AddColumn("dbo.Conformities", "TempResult_Id", c => c.Int());
            CreateIndex("dbo.Conformities", "TempResult_Id");
            AddForeignKey("dbo.Conformities", "TempResult_Id", "dbo.TempResults", "Id");
            DropColumn("dbo.Conformities", "InputTeamName");
            DropColumn("dbo.Conformities", "ExistedTeam_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conformities", "ExistedTeam_Id", c => c.Int());
            AddColumn("dbo.Conformities", "InputTeamName", c => c.String());
            DropForeignKey("dbo.TempResults", "TempSportEvent_Id", "dbo.TempSportEvents");
            DropForeignKey("dbo.TempSportEvents", "SportType_Id", "dbo.SportTypes");
            DropForeignKey("dbo.TempResults", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Conformities", "TempResult_Id", "dbo.TempResults");
            DropIndex("dbo.TempSportEvents", new[] { "SportType_Id" });
            DropIndex("dbo.TempResults", new[] { "TempSportEvent_Id" });
            DropIndex("dbo.TempResults", new[] { "Team_Id" });
            DropIndex("dbo.Conformities", new[] { "TempResult_Id" });
            DropColumn("dbo.Conformities", "TempResult_Id");
            DropColumn("dbo.Conformities", "ExistedName");
            DropColumn("dbo.Conformities", "InputName");
            DropTable("dbo.TempSportEvents");
            DropTable("dbo.TempResults");
            CreateIndex("dbo.Conformities", "ExistedTeam_Id");
            AddForeignKey("dbo.Conformities", "ExistedTeam_Id", "dbo.Teams", "Id");
        }
    }
}
