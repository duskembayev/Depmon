using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Depmon.Server.Database
{
    public abstract class Repository<T> : IRepository<T>
    {
        protected IDbConnection _connection;
        protected IDbTransaction _transaction;

        private string _tableName;

        protected Repository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;

            GetTableName();
        }

        public abstract void Save(T entity);

        public abstract void InsertMany(params T[] entities);


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
            return SqlMapper.Query<T>(_connection, sql, null, _transaction);
            //return Connection.Query<T>(sql);
        }

        public virtual T GetById(int id)
        {
            var sql = $"SELECT * FROM {_tableName} WHERE Facts.Id = @id";
            //var query = Connection.Query<T>(sql, new { id = id });
            var query = SqlMapper.Query<T>(_connection, sql, new { id = id }, _transaction);
            return query.ElementAtOrDefault(0);
        }

        public virtual void Delete(T entity)
        {
            var sql = $"DELETE FROM {_tableName} WHERE Id = @Id";

            SqlMapper.Execute(_connection, sql, entity, _transaction);

            //Connection.Execute(sql, entity);
        }

        public void Dispose()
        {
            try
            {
                _connection.Close();
            }
            finally
            {
                _connection = null;
                _transaction = null;
            }
        }
    }
}
