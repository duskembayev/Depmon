using System;
using System.Configuration;

namespace Depmon.Server.Collector.Configuration
{
    public class EveryDay: ConfigurationElement
    {
        [ConfigurationProperty("time", IsRequired = true)]
        public TimeSpan Time => (TimeSpan)base["time"];
    }
}
