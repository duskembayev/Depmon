using System;
using System.Collections.Generic;

namespace Depmon.Server.Database
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void InsertMany(params T[] entity);

        void Save(T entity);

        void Delete(T entity);
    }
}
