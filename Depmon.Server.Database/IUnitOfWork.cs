namespace Depmon.Server.Database
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>();
    }
}
