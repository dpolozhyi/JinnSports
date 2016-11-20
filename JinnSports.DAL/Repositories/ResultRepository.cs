using System;
using System.Collections.Generic;
using System.Linq;
using JinnSports.DAL.EF;
using JinnSports.DAL.Entities;
using JinnSports.DAL.Interfaces;
using System.Data.Entity;

namespace JinnSports.DAL.Repositories
{
    public class ResultRepository : IRepository<Result>
    {
        private SportsContext db;

        public ResultRepository(SportsContext context)
        {
            this.db = context;
        }

        public IEnumerable<Result> GetAll()
        {
            return db.Results;
        }

        public Result Get(int id)
        {
            return db.Results.Find(id);
        }

        public void Create(Result data)
        {
            db.Results.Add(data);
        }
        public void Update(Result data)
        {
            db.Entry(data).State = EntityState.Modified;
        }

        public IEnumerable<Result> Find(Func<Result, Boolean> predicate)
        {
            return db.Results.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Result data = db.Results.Find(id);
            if (data != null)
                db.Results.Remove(data);
        }
    }
}
