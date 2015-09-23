using System.Configuration;
using Depmon.Server.Collector.Configuration;

namespace Depmon.Server.Collector.Impl.Configuration
{
    public class ConfigReader : IConfigReader
    {
        public MonitoringSection Read()
        {
            return (MonitoringSection)ConfigurationManager.GetSection("monitoring");
        }
    }
}
