﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace Depmon.Server.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection _connection;
        public IDbTransaction _transaction;

        public UnitOfWork(string connectionString, bool requireTransaction = true)
        {
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();

            if (requireTransaction)
            {
                BeginTransaction();
            }
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                return;
            }
            _transaction = _connection.BeginTransaction();
        }
        public void CommitChanges()
        {
            _transaction?.Commit();
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
