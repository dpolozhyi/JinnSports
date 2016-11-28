using System;
using JinnSports.DataAccessInterfaces;
using JinnSports.DAL.EF;
using JinnSports.DAL.Entities;

namespace JinnSports.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private SportsContext db;
        private IRepository<Team> Teams;
        private IRepository<Result> Results;
        private IRepository<CompetitionEvent> CompetitionEvents;
        private IRepository<SportType> SportTypes;

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
