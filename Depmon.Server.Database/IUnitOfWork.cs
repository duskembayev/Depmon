using System;

namespace Depmon.Server.Database
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        void CommitChanges();

        IRepository<T> GetRepository<T>();
    }
}
