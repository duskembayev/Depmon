using Depmon.Server.Collector.Configuration;

namespace Depmon.Server.Collector
{
    public interface IEngine
    {
        void Start(Settings config);
        void Stop();
    }
}
