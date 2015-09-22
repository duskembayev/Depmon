using System.Collections.Generic;

namespace Depmon.Server.Database
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Save(T entity);

        void Delete(T entity);
    }
}
