using System;
using System.Collections.Generic;
using System.Data;

namespace Depmon.Server.Database
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll(IDbConnection session);

        T GetById(IDbConnection session, int id);

        void InsertMany(IDbConnection session, params T[] entities);

        void Save(IDbConnection session, T entity);

        void Delete(IDbConnection session, T entity);
    }
}
