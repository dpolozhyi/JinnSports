using System;
using System.Collections.Generic;

namespace JinnSports.DataAccessInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Set<T>() where T : class;
        void SaveChanges();
    }
}
