using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace Depmon.Server.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        protected IDbConnection connection;
        protected IDbTransaction transaction;

        public UnitOfWork(bool requireTransaction = true)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["depmon"];
            if (connectionString == null)
            {
                throw new ApplicationException("Connection string 'depmon' not found");
            }
            connection = new SQLiteConnection(connectionString.ConnectionString);
            connection.Open();

            if (requireTransaction)
            {
                BeginTransaction();
            }
        }

        public void BeginTransaction()
        {
            if (transaction != null)
            {
                return;
            }
            transaction = connection.BeginTransaction();
        }
        public void CommitChanges()
        {
            if (transaction != null)
            {
                transaction.Commit();
            }
        }

        public IRepository<T> GetRepository<T>()
        {
            var type = typeof(T).Name;
            
            object repository;

            //TODO: придумать другой вариант
            switch (type)
            {
                case "Fact":
                    repository = new FactRepository(connection, transaction);
                    break;
                case "Report":
                    repository = new ReportRepository(connection, transaction);
                    break;
                default:
                    repository = null;
                    break;
            }

            return (IRepository<T>)repository;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                    transaction = null;
                }
                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }
            }
        }
    }
}
