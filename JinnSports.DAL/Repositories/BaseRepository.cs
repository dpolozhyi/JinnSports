using System.Collections.Generic;
using System.Linq;
using JinnSports.DAL.EFContext;
using System.Data.Entity;
using JinnSports.DataAccessInterfaces.Interfaces;
using System.Linq.Expressions;
using System;

namespace JinnSports.DAL.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private DbSet<T> dbSet;

        public BaseRepository(SportsContext db)
        {
            this.DbSet = db.Set<T>();
        }
        private DbSet<T> DbSet { get; }

        public IList<T> GetAll()
        {
            return this.DbSet.ToList();
        }

        public IList<T> GetAll(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault();
        }

        public T GetByID(int id)
        {
            return this.DbSet.Find(id);
        }

        public void Add(T item)
        {
            this.DbSet.Add(item);
        }

        public void AddAll(T[] items)
        {
            this.DbSet.AddRange(items);
        }

        public void Remove(T item)
        {
            this.DbSet.Remove(item);
        }
    }
}
