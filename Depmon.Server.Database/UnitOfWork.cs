using System.Collections.Generic;
using System.Data;

namespace Depmon.Server.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private Dictionary<string, object> _repositories;

        public UnitOfWork()
        {
            _connection = new ConnectionFactory().Create();
            _repositories = new Dictionary<string, object>();
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
