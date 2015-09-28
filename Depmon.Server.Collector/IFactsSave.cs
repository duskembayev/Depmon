using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector
{
    public interface IFactsSave
    {
        void Save(Fact[] facts);
    }
}
