using System;
using System.Collections.Generic;

namespace JinnSports.DataAccessInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
