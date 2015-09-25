using System;

namespace Depmon.Server.Database
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        void CommitChanges();

        void SetRepository<T>(IRepository<T> repository);
    }
}
