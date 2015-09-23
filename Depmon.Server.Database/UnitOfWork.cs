using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace Depmon.Server.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private Dictionary<string, object> _repositories;

        public UnitOfWork()
        {
            _repositories = new Dictionary<string, object>();

            var connectionString = ConfigurationManager.ConnectionStrings["depmon"];
            if (connectionString == null)
            {
                throw new ApplicationException("Connection string 'depmon' not found");
            }
            _connection = new SQLiteConnection(connectionString.ConnectionString);
            _connection.Open();
        }

        public IRepository<T> GetRepository<T>()
        {
            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                object repository;

                //TODO: придумать другой вариант
                switch (type)
                {
                    case "Fact":
                        repository = new FactRepository(_connection);
                        break;
                    case "Report":
                        repository = new ReportRepository(_connection);
                        break;
                    default:
                        repository = null;
                        break;
                }

                _repositories.Add(type, repository);
            }

            return (IRepository<T>)_repositories[type];
        }

        public void Dispose()
        {
            _connection = null;
            _repositories = null;
        }
    }
}
