using System.Data;
using System.Data.SQLite;

namespace Depmon.Server.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _session;

        public IDbConnection Session => _session;

        public UnitOfWork(string connectionString)
        {
            _session = new SQLiteConnection(connectionString);
            _session.Open();
        }

        public IDbTransaction BeginTransaction()
        {
            return _session.BeginTransaction();
        }

        public void Dispose()
        {
            _session?.Dispose();
            _session = null;
        }
    }
}
