using System;
using System.Collections.Generic;
using System.Linq;
using JinnSports.DAL.EF;
using JinnSports.DAL.Entities;
using JinnSports.DAL.Interfaces;
using System.Data.Entity;

namespace JinnSports.DAL.Repositories
{
    public class CompetitionEventRepository : BaseRepository<CompetitionEvent>
    {
        public CompetitionEventRepository(SportsContext context) : base(context)
        {

        }
    }
}
