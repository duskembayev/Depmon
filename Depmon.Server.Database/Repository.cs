using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Depmon.Server.Database
{
    public abstract class Repository<T> : IRepository<T>
    {
        protected abstract string TableName { get; }

        public abstract void Save(IDbConnection session, T entity);

        public abstract void InsertMany(IDbConnection session, params T[] entities);

        public virtual IEnumerable<T> GetAll(IDbConnection session)
        {
            var sql = $"SELECT * FROM {TableName}";
            return session.Query<T>(sql);
        }

        public virtual T GetById(IDbConnection session, int id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Facts.Id = @id";
            var query = session.Query<T>(sql, new { id = id });
            return query.ElementAtOrDefault(0);
        }

        public virtual void Delete(IDbConnection session, T entity)
        {
            var sql = $"DELETE FROM {TableName} WHERE Id = @Id";

            session.Execute(sql, entity);
        }
    }
}
