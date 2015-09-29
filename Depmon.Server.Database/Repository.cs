using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Depmon.Server.Database
{
    public abstract class Repository<T> : IRepository<T>
    {
        protected IDbConnection _connection;

        private string _tableName;

        public abstract void Save(T entity);

        public abstract void InsertMany(params T[] entities);

        public void InitConnection(IDbConnection connection)
        {
            _connection = connection;
            GetTableName();
        }

        private void GetTableName()
        {
            string sql = "SELECT name FROM sqlite_master WHERE type = 'table'";
            IEnumerable<string> names = _connection.Query<string>(sql);
            string type = typeof (T).Name;
            _tableName = names.FirstOrDefault(s => s.Contains(type));
        }

        public virtual IEnumerable<T> GetAll()
        {
            var sql = $"SELECT * FROM {_tableName}";
            return _connection.Query<T>(sql);
        }

        public virtual T GetById(int id)
        {
            var sql = $"SELECT * FROM {_tableName} WHERE Facts.Id = @id";
            var query = _connection.Query<T>(sql, new { id = id });
            return query.ElementAtOrDefault(0);
        }

        public virtual void Delete(T entity)
        {
            var sql = $"DELETE FROM {_tableName} WHERE Id = @Id";

            _connection.Execute(sql, entity);
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection = null;
        }
    }
}
