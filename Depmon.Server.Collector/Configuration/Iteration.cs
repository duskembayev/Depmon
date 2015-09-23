using System;
using System.Configuration;

namespace Depmon.Server.Collector.Configuration
{
    public class Iteration : ConfigurationElement
    {
        [ConfigurationProperty("delay")]
        public TimeSpan Delay => (TimeSpan)base["delay"];
    }
}
