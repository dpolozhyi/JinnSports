using System;
using System.Data.Entity;
using JinnSports.Entities.Entities;
using JinnSports.Entities.Entities.Temp;
using System.Collections.Generic;

namespace JinnSports.DAL.EFContext
{
    public class SportsDbInitializer : CreateDatabaseIfNotExists<SportsContext>
    {
        protected override void Seed(SportsContext context)
        {           
            base.Seed(context);
        }
    }
}
