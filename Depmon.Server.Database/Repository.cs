using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Depmon.Server.Database
{
    public abstract class Repository<T> : IRepository<T>
    {
        public IDbConnection Connection { get; private set; }

        private string _tableName;

        protected Repository(IDbConnection connection)
        {
            Connection = connection;

            GetTableName();
        }

        public abstract void Save(T entity);

        public abstract void InsertMany(params T[] entities);


        private void GetTableName()
        {
            string sql = "SELECT name FROM sqlite_master WHERE type = 'table'";
            IEnumerable<string> names = Connection.Query<string>(sql);
            string type = typeof (T).Name;
            _tableName = names.FirstOrDefault(s => s.Contains(type));
        }

        public virtual IEnumerable<T> GetAll()
        {
            var sql = $"SELECT * FROM {_tableName}";
            return Connection.Query<T>(sql);
        }

        public virtual T GetById(int id)
        {
            var sql = $"SELECT * FROM {_tableName} WHERE Facts.Id = @id";
            var query = Connection.Query<T>(sql, new { id = id });
            return query.ElementAtOrDefault(0);
        }

        public virtual void Delete(T entity)
        {
            var sql = $"DELETE FROM {_tableName} WHERE Id = @Id";
            Connection.Execute(sql, entity);
        }

        public void Dispose()
        {
            Connection = null;
        }
    }
}
