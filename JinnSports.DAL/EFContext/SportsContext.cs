using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using JinnSports.Entities.Entities;

namespace JinnSports.DAL.EFContext
{
    public class SportsContext : DbContext
    {
        static SportsContext()
        {
            Database.SetInitializer(new SportsDbInitializer());
        }
       
        public SportsContext(string connectionString) : base(connectionString)
        {
            // Getting root folder for solution from Parser.App
            // TODO: make work from all projects
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\"));
            connectionString = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
        }

        public DbSet<CompetitionEvent> CompetitionEvents { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<SportType> SportTypes { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}