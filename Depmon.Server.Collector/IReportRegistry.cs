using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector
{
    public interface IReportRegistry
    {
        void Save(Fact[] facts);
    }
}
