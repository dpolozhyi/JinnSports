using System;
using JinnSports.DAL.Entities;
using JinnSports.DAL.Interfaces;
using JinnSports.DAL.EF;

namespace JinnSports.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private SportsContext db;
        private SportTypeRepository sportTypeRepository;
        private TeamRepository teamRepository;
        private ResultRepository resultRepository;
        private CompetitionEventRepository competitionEventRepository;

       
        public EFUnitOfWork()
        {
            db = new SportsContext();
        }

        public IRepository<SportType> SportTypes
        {
            get
            {
                if (sportTypeRepository == null)
                    sportTypeRepository = new SportTypeRepository(db);
                return sportTypeRepository;
            }
        }

        public IRepository<Team> Teams
        {
            get
            {
                if (teamRepository == null)
                    teamRepository = new TeamRepository(db);
                return teamRepository;
            }
        }

        public IRepository<Result> Results
        {
            get
            {
                if (resultRepository == null)
                    resultRepository = new ResultRepository(db);
                return resultRepository;
            }
        }

        public IRepository<CompetitionEvent> CompetitionEvents
        {
            get
            {
                if (competitionEventRepository == null)
                    competitionEventRepository = new CompetitionEventRepository(db);
                return competitionEventRepository;
            }
        }
        public void Save()
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
