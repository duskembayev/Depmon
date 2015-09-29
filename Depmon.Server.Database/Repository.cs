using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Depmon.Server.Database
{
    public abstract class Repository<T> : IRepository<T>
    {
        protected IDbConnection _connection;

        protected abstract string TableName { get; }

        public abstract void Save(T entity);

        public abstract void InsertMany(params T[] entities);

        public void InitConnection(IDbConnection connection)
        {
            _connection = connection;
        }

        public virtual IEnumerable<T> GetAll()
        {
            var sql = $"SELECT * FROM {TableName}";
            return _connection.Query<T>(sql);
        }

        public virtual T GetById(int id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Facts.Id = @id";
            var query = _connection.Query<T>(sql, new { id = id });
            return query.ElementAtOrDefault(0);
        }

        public virtual void Delete(T entity)
        {
            var sql = $"DELETE FROM {TableName} WHERE Id = @Id";

            _connection.Execute(sql, entity);
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection = null;
        }
    }
}
