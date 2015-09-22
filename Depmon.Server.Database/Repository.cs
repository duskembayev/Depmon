using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Depmon.Server.Database
{
    public abstract class Repository<T> : IRepository<T>
    {
        public IDbConnection Connection { get; private set; }

        protected int LastId => (int)((SQLiteConnection)Connection).LastInsertRowId;

        protected Repository(IDbConnection connection)
        {
            Connection = connection;
        }

        public abstract IEnumerable<T> GetAll();

        public abstract T GetById(int id);

        public abstract void Save(T entity);

        public abstract void Delete(T entity);
    }
}
