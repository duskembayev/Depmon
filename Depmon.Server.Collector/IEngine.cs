using Depmon.Server.Collector.Configuration;

namespace Depmon.Server.Collector
{
    public interface IEngine
    {
        void Start(MonitoringSection config);
        void Stop();
    }
}
