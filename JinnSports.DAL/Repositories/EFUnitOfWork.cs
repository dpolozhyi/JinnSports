using System;
using JinnSports.DataAccessInterfaces;
using JinnSports.DAL.EFContext;
using JinnSports.DAL.Entities;

namespace JinnSports.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private SportsContext db;
        public IRepository<Team> Teams
        {
            get;
        }
        public IRepository<Result> Results
        {
            get;
        }
        public IRepository<CompetitionEvent> CompetitionEvents
        {
            get;
        }
        public IRepository<SportType> SportTypes
        {
            get;
        }

        public EFUnitOfWork()
        {
            db = new SportsContext();
            Teams = new BaseRepository<Team>(db.Teams);
            Results = new BaseRepository<Result>(db.Results);
            CompetitionEvents = new BaseRepository<CompetitionEvent>(db.CompetitionEvents);
            SportTypes = new BaseRepository<SportType>(db.SportTypes);
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
