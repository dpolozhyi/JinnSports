using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using JinnSports.Entities;

namespace JinnSports.DAL.EFContext
{
    public class SportsContext : DbContext
    {
        static SportsContext()
        {
            Database.SetInitializer(new SportsDbInitializer());
        }
        private static string GetConnectionString(string connectionString)
        {
            connectionString = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            return connectionString;
        }
        public SportsContext(string connectionString) : base(SportsContext.GetConnectionString(connectionString))
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\"));
            // Getting root folder for solution from Parser.App
            // TODO: make work from all projects     
        }

        public DbSet<CompetitionEvent> CompetitionEvents { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<SportType> SportTypes { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}