using System;
using System.Collections.Generic;
using System.Data.Entity;
using JinnSports.DAL.Entities;

namespace JinnSports.DAL.EF
{
    public class SportsContext : DbContext
    {
        public DbSet<CompetitionEvent> CompetitionEvents;
        public DbSet<Result> Results;
        public DbSet<SportType> SportTypes;
        public DbSet<Team> Teams;

        static SportsContext()
        {
            Database.SetInitializer<SportsContext>(new StoreDbInitializer());
        }
        public SportsContext(string connectionString)
            : base(connectionString)
        {
        }
        public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<SportsContext>
        {
            protected override void Seed(SportsContext db)
            {
                db.SaveChanges();
            }
        }
    }
}
