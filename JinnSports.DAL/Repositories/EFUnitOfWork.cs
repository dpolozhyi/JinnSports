using System;
using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.DAL.EFContext;

namespace JinnSports.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private SportsContext db;

        private bool disposed = false;

        public EFUnitOfWork(string connectionString)
        {
            this.db = new SportsContext(connectionString);            
        }

        public IRepository<T> Set<T>() where T : class
        {
            return new BaseRepository<T>(this.db);
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }        
    }
}
