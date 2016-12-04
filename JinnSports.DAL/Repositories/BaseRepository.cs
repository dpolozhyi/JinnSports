using System.Collections.Generic;
using System.Linq;
using JinnSports.DAL.EFContext;
using System.Data.Entity;
using JinnSports.DataAccessInterfaces.Interfaces;

namespace JinnSports.DAL.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    { 
        public BaseRepository(SportsContext db)
        {
            this.DbSet = db.Set<T>();
        }
        private DbSet<T> DbSet { get; }

        public IList<T> GetAll()
        {
            return this.DbSet.ToList();
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
