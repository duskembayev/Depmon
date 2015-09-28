using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace Depmon.Server.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection _connection;
        public IDbTransaction _transaction;

        public UnitOfWork(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();
        }

        public IDbTransaction BeginTransaction()
        {
            if (_transaction == null)
                _transaction = _connection.BeginTransaction();

            return _transaction;
        }

        public void SetRepository<T>(IRepository<T> repository)
        {
            ((Repository<T>)repository).InitConnection(_connection, _transaction);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
        }
    }
}
