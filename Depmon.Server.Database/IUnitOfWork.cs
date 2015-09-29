using System;
using System.Data;

namespace Depmon.Server.Database
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Session { get; }

        IDbTransaction BeginTransaction();
    }
}
