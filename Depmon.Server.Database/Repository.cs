using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Depmon.Server.Database
{
    public abstract class Repository<T> : IRepository<T>
    {
        protected abstract string TableName { get; }
        protected IUnitOfWork _unitOfWork;

        protected Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public abstract int Save(T entity);

        public abstract void InsertMany(params T[] entities);

        public virtual IEnumerable<T> GetAll()
        {
            var sql = $"SELECT * FROM {TableName}";
            return _unitOfWork.Session.Query<T>(sql);
        }

        public virtual T GetById(int id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE {TableName}.Id = @id";
            var query = _unitOfWork.Session.Query<T>(sql, new { id = id });
            return query.ElementAtOrDefault(0);
        }

        public virtual void Delete(T entity)
        {
            var sql = $"DELETE FROM {TableName} WHERE Id = @Id";

            _unitOfWork.Session.Execute(sql, entity);
        }

        public void Dispose()
        {
            _unitOfWork = null;
        }
    }
}
