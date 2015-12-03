using Depmon.Server.Domain;

namespace Depmon.Server.Collector
{
    public interface IReportRegistry
    {
        Report Save(Fact[] facts);
    }
}
