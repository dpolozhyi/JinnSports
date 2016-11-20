using System;
using System.Collections.Generic;
using System.Linq;
using JinnSports.DAL.EF;
using JinnSports.DAL.Entities;
using JinnSports.DAL.Interfaces;
using System.Data.Entity;

namespace JinnSports.DAL.Repositories
{
    public class CompetitionEventRepository : IRepository<CompetitionEvent>
    {
        private SportsContext db;

        public CompetitionEventRepository(SportsContext context)
        {
            this.db = context;
        }

        public IEnumerable<CompetitionEvent> GetAll()
        {
            return db.CompetitionEvents;
        }

        public CompetitionEvent Get(int id)
        {
            return db.CompetitionEvents.Find(id);
        }

        public void Create(CompetitionEvent data)
        {
            db.CompetitionEvents.Add(data);
        }
        public void Update(CompetitionEvent data)
        {
            db.Entry(data).State = EntityState.Modified;
        }

        public IEnumerable<CompetitionEvent> Find(Func<CompetitionEvent, Boolean> predicate)
        {
            return db.CompetitionEvents.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            CompetitionEvent data = db.CompetitionEvents.Find(id);
            if (data != null)
                db.CompetitionEvents.Remove(data);
        }
    }
}
