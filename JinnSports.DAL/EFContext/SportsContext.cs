using System.Configuration;
using System.Data.Entity;
using JinnSports.Entities.Entities;

namespace JinnSports.DAL.EFContext
{
    public class SportsContext : DbContext
    {
        public SportsContext() : base(GetConnectionString("SportsContext"))
        {
            Database.SetInitializer(new SportsDbInitializer());
        }

        public SportsContext(string connectionName) : base(GetConnectionString(connectionName))
        {
            Database.SetInitializer(new SportsDbInitializer());
        }

        public DbSet<SportEvent> SportEvents { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<SportType> SportTypes { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamName> TeamNames { get; set; }

        public DbSet<SportEventName> SportEventNames { get; set; }

        public DbSet<Conformity> Conformities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Result>().HasRequired(p => p.Team);
            modelBuilder.Entity<Result>().HasRequired(p => p.SportEvent);

            modelBuilder.Entity<Team>().HasRequired(p => p.SportType);
            modelBuilder.Entity<Team>().HasRequired(p => p.Name);
            modelBuilder.Entity<Team>().Property(p => p.Name).HasMaxLength(0);

            modelBuilder.Entity<SportType>().HasRequired(p => p.Name);

            modelBuilder.Entity<SportEvent>().HasRequired(p => p.SportType);
            modelBuilder.Entity<SportEvent>().HasRequired(p => p.Name);

            modelBuilder.Entity<TeamName>().HasRequired(p => p.Name);
            modelBuilder.Entity<SportEventName>().HasRequired(p => p.Name);

            base.OnModelCreating(modelBuilder);
        }

        private static string GetConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
    }
}