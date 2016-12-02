using System;
using System.Collections.Generic;

namespace JinnSports.DataAccessInterfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        T GetByID(int id);
        void Add(T item);
        void AddAll(T[] items);
        void Remove(T item);
    }
}
