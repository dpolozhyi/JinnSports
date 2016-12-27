namespace JinnSports.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntputTeamNameStringConformity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Conformities", "InputTeam_Id", "dbo.Teams");
            DropIndex("dbo.Conformities", new[] { "InputTeam_Id" });
            AddColumn("dbo.Conformities", "InputTeamName", c => c.String());
            DropColumn("dbo.Conformities", "InputTeam_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conformities", "InputTeam_Id", c => c.Int());
            DropColumn("dbo.Conformities", "InputTeamName");
            CreateIndex("dbo.Conformities", "InputTeam_Id");
            AddForeignKey("dbo.Conformities", "InputTeam_Id", "dbo.Teams", "Id");
        }
    }
}
