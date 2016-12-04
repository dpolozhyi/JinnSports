using System;

namespace JinnSports.DataAccessInterfaces.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Set<T>() where T : class;
        void SaveChanges();
    }
}
