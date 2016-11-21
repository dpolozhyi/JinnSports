using System;
using JinnSports.DAL.Entities;
namespace JinnSports.DAL.EF
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SportsContext : DbContext
    {
        public DbSet<CompetitionEvent> CompetitionEvents;
        public DbSet<Result> Results;
        public DbSet<SportType> SportTypes;
        public DbSet<Team> Teams;

        public SportsContext()
            : base("name=SportsContext.cs")
        {
        }
        public SportsContext(string connectionString)
            : base(connectionString)
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}