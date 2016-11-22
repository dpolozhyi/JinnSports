using System;
using System.Collections.Generic;
using System.Linq;
using JinnSports.DAL.EF;
using JinnSports.DAL.Entities;
using JinnSports.DAL.Interfaces;
using System.Data.Entity;

namespace JinnSports.DAL.Repositories
{
    public class ResultRepository : BaseRepository<Result>
    {
        public ResultRepository(SportsContext context) : base(context)
        {

        }
    }
}
