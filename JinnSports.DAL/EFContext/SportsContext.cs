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
       
        public SportsContext(string connectionName) : base(GetConnectionString(connectionName))
        {
            Database.SetInitializer(new SportsDbInitializer());
        }

        public DbSet<CompetitionEvent> CompetitionEvents { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<SportType> SportTypes { get; set; }
        public DbSet<Team> Teams { get; set; }

        private static string GetConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
    }
}