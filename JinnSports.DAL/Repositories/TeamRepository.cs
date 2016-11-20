using System;
using System.Collections.Generic;
using System.Linq;
using JinnSports.DAL.EF;
using JinnSports.DAL.Entities;
using JinnSports.DAL.Interfaces;
using System.Data.Entity;

namespace JinnSports.DAL.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        private SportsContext db;

        public TeamRepository(SportsContext context)
        {
            this.db = context;
        }

        public IEnumerable<Team> GetAll()
        {
            return db.Teams;
        }

        public Team Get(int id)
        {
            return db.Teams.Find(id);
        }

        public void Create(Team data)
        {
            db.Teams.Add(data);
        }
        public void Update(Team data)
        {
            db.Entry(data).State = EntityState.Modified;
        }

        public IEnumerable<Team> Find(Func<Team, Boolean> predicate)
        {
            return db.Teams.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Team data = db.Teams.Find(id);
            if (data != null)
                db.Teams.Remove(data);
        }
    }
}
