using System;

namespace Depmon.Server.Database
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>();
    }
}
