using System.Configuration;
using Depmon.Server.Collector.Configuration;

namespace Depmon.Server.Collector.Impl.Configuration
{
    public class SettingsReader : ISettingsReader
    {
        public Settings Read()
        {
            return (Settings)ConfigurationManager.GetSection("settings");
        }
    }
}
