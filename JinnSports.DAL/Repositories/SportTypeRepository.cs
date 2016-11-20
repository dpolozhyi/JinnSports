using System;
using System.Collections.Generic;
using System.Linq;
using JinnSports.DAL.EF;
using JinnSports.DAL.Entities;
using JinnSports.DAL.Interfaces;
using System.Data.Entity;

namespace JinnSports.DAL.Repositories
{
    public class SportTypeRepository : IRepository<SportType>
    {
        private SportsContext db;

        public SportTypeRepository(SportsContext context)
        {
            this.db = context;
        }

        public IEnumerable<SportType> GetAll()
        {
            return db.SportTypes;
        }

        public SportType Get(int id)
        {
            return db.SportTypes.Find(id);
        }

        public void Create(SportType data)
        {
            db.SportTypes.Add(data);
        }
        public void Update(SportType data)
        {
            db.Entry(data).State = EntityState.Modified;
        }

        public IEnumerable<SportType> Find(Func<SportType, Boolean> predicate)
        {
            return db.SportTypes.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            SportType data = db.SportTypes.Find(id);
            if (data != null)
                db.SportTypes.Remove(data);
        }
    }
}
