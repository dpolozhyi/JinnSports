using System;
using System.Collections.Generic;
using JinnSports.DAL.Entities;

namespace JinnSports.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Team> Teams { get; }
        IRepository<CompetitionEvent> CompetitionEvents { get; }
        IRepository<Result> Results { get; }
        IRepository<SportType> SportTypes { get; }
        void Save();
    }
}
