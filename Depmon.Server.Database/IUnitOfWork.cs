using System;
using System.Data;

namespace Depmon.Server.Database
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction BeginTransaction();

        void SetRepository<T>(IRepository<T> repository);
    }
}
