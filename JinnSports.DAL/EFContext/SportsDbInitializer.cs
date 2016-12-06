using System.Collections.Generic;
using System.Data.Entity;
using JinnSports.Entities;

namespace JinnSports.DAL.EFContext
{
    public class SportsDbInitializer : DropCreateDatabaseAlways<SportsContext>
    {
        protected override void Seed(SportsContext context)
        {
            context.Teams.Add(new Team { SportType = null, Name = "kek", Results = new List<Result> { new Result { Score = "3:0" } } });
            context.Teams.Add(new Team { SportType = null, Name = "kek2", Results = null });
            context.Teams.Add(new Team { SportType = null, Name = "kek3", Results = null });
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
