using System;
using System.Configuration;

namespace Depmon.Server.Collector.Configuration
{
    public class Mailbox : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name => (string)base["name"];

        [ConfigurationProperty("server", IsRequired = true)]
        public string Server => (string)base["server"];

        [ConfigurationProperty("port", DefaultValue = 110)]
        public int Port => (int)base["port"];

        [ConfigurationProperty("ssl", DefaultValue = false)]
        public bool Ssl => (bool)base["ssl"];

        [ConfigurationProperty("username", IsRequired = false)]
        public string Username => (string)base["username"];

        [ConfigurationProperty("password", IsRequired = false)]
        public string Password => (string)base["password"];

        [ConfigurationProperty("timeout", DefaultValue = 1000)]
        public int Timeout => (int)base["timeout"];

        [ConfigurationProperty("capacity", DefaultValue = 10)]
        public int Capacity => (int)base["capacity"];

        [ConfigurationProperty("delay", DefaultValue = "00:00:10")]
        public TimeSpan Delay => (TimeSpan)base["delay"];

    }
}