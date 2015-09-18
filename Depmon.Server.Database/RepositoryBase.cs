using System;
using System.Data;
using System.Data.SQLite;

namespace Depmon.Server.Database
{
    public abstract class RepositoryBase : IDisposable
    {
        public IDbConnection Connection { get; private set; }

        protected int LastId => (int) ((SQLiteConnection) Connection).LastInsertRowId;

        protected RepositoryBase(IDbConnection connection)
        {
            Connection = connection;
        }

        public void Dispose()
        {
            Connection = null;
        }
    }
}
