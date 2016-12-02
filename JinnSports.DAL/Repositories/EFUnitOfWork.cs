using System;
using JinnSports.DataAccessInterfaces;
using JinnSports.DAL.EFContext;
using JinnSports.Entities;

namespace JinnSports.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private SportsContext db;
        
        public EFUnitOfWork()
        {
            db = new SportsContext();            
        }

        public IRepository<T> Set<T>() where T : class
        {
            return new BaseRepository<T>(db);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }        
    }
}
