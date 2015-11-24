using Depmon.Server.Domain;

namespace Depmon.Server.Collector
{
    public interface IReportRegistry
    {
        void Save(Fact[] facts);
    }
}
